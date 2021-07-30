using System;
using System.Collections.Generic;


namespace WpfMgCrypt
{
  
    class Encryption : OperationWithFiles
    {
        public string GetPas()
        {
            return GenPas();
        }

        public Dictionary<string, string> GetPubKey(string WayToAlphabet, string SpaceKey)
        {
            return GetPublicKey(WayToAlphabet, SpaceKey );
        }

        private string GenPas()
        {
            int[] arr = new int[16];
            Random rnd = new Random();
            string Password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                Password += (char)arr[i];
            }

            return Password;
        }

        private Dictionary<string, string> GetPublicKey(string WayToAlphabet, string SpaceKey)
        {
            string Alphabet = Reading(WayToAlphabet);

            // Alphabet = Alphabet.Remove(0, 1);
            Alphabet = Alphabet.Replace(SpaceKey, "");
            Alphabet += Alphabet.ToUpper(); // bag double space
            Alphabet = Alphabet + SpaceKey;
            
            Dictionary<string, string> pubKey = new Dictionary<string, string>();

            for (int i = 0; i < Alphabet.Length; i++)
                pubKey.Add(GenPas(), Alphabet[i].ToString());

            return pubKey;
        }
    }
}
