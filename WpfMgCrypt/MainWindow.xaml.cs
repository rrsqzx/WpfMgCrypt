using System;
using System.Windows;
using System.Collections.Generic;

namespace WpfMgCrypt
{
    

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        OperationWithFiles OperationWithFiles = new OperationWithFiles();
        Encryption encryption = new Encryption();

        const string way_message = @"files\File.txt";
        const string way_letters = @"files\Letters.txt";
        const string way_code_info = @"files\Code_Info.txt";
        string way_to_decryption_script;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBox2.Clear();

            Dictionary<string, string> pubKey = new Dictionary<string, string>();
            Dictionary<string, string> pubKeyDictionaryToEncrOnCodeInfo = new Dictionary<string, string>();// словарь для шифровки по приватному ключу

            if (checkBox1.IsChecked == true)// зашифровка по заданому ключу
            {
                string CodeInfo = OperationWithFiles.Reading(way_to_decryption_script);
                String[] PubKeyArr = CodeInfo.Split(' ');

                for (int i = 1; i < PubKeyArr.Length - 1; i += 2)
                    pubKeyDictionaryToEncrOnCodeInfo.Add(PubKeyArr[i], PubKeyArr[i + 1]);

                string Message = textBox1.Text;
                Message = Message.Replace(' ', '0');

                OperationWithFiles.DeletTextInTxt(way_message);
                OperationWithFiles.Stream1(way_message, Message);

                for (int i = 0; i < Message.Length; i++)
                {
                    foreach (var j in pubKeyDictionaryToEncrOnCodeInfo)
                    {
                        if (Message[i].ToString() == j.Value)
                        {
                            textBox2.Text += j.Key;
                        }
                    }
                }
            }

            else // стандартная зашифровка с заданием файла
            {
                pubKey = encryption.GetPubKey(way_letters, "0");
                string PubKey = "";

                foreach (var i in pubKey)
                {
                    PubKey += " " + i.Key + " " + i.Value;
                }

                OperationWithFiles.DeletTextInTxt(way_code_info);
                OperationWithFiles.Stream1(way_code_info, PubKey);

                string Message = textBox1.Text;
                Message = Message.Replace(' ', '0');
              

                OperationWithFiles.DeletTextInTxt(way_message);
                OperationWithFiles.Stream1(way_message, Message);

                for (int i = 0; i < Message.Length; i++)
                {
                    foreach (var j in pubKey)
                    {
                        if (Message[i].ToString() == j.Value)
                        {
                            textBox2.Text += j.Key;
                        }
                    }
                }
            }

            Clipboard.SetData(DataFormats.Text, (Object)textBox2.Text);
        }

        string encryptmes;
        private void Button_Click_1(object sender, RoutedEventArgs e)// decode
        {
            encryptmes = "";
            textBox2.Clear();

            string decryption_script = OperationWithFiles.Reading(way_to_decryption_script);

            String[] PubKeyArr = decryption_script.Split(' ');
            
            Dictionary<string, string> PubKey = new Dictionary<string, string>();

            for (int i = 1; i < PubKeyArr.Length; i += 2)
                PubKey.Add(PubKeyArr[i], PubKeyArr[i + 1]);

            string Message = textBox1.Text;

            List<string> MessageList = new List<string>();

            for (int i = 0; i < Message.Length; i += 16)
                MessageList.Add(Message.Substring(i, 16));

            for (int i = 0; i < MessageList.Count; i++)
                foreach (var dictionary in PubKey)
                    if (MessageList[i] == dictionary.Key)
                        encryptmes += dictionary.Value;

            encryptmes = encryptmes.Replace('0', ' ');
            textBox2.Text = encryptmes;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            way_to_decryption_script = OperationWithFiles.GetWayDialog();
            textBox3.Text = way_to_decryption_script;

        }
    }
}
