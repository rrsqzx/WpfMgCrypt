using System;
using System.Text;
using Microsoft.Win32;
using System.IO;

namespace WpfMgCrypt
{
    class OperationWithFiles
    {
        public string GetWayDialog()
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.Multiselect = true;
            filedialog.Filter = "Textfiles|*.txt|All files|*.*";
            filedialog.DefaultExt = ". txt";
            Nullable<bool> dialogOK = filedialog.ShowDialog();
            string sFilenames = "";

            if (dialogOK == true)
            {

                foreach (string sFilename in filedialog.FileNames)
                {
                    sFilenames += ";" + sFilename;
                }
                sFilenames = sFilenames.Substring(1);

            }
            return sFilenames;
        }

        public void DeletTextInTxt(string path)
        {
            using (var stream = new FileStream(path, FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {

                }
            }
        }

        public void Stream2(string WayToFile, string Text_to_sream)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(WayToFile, true, Encoding.Default))
                {
                    sw.WriteLine(Text_to_sream);

                }

            }

            catch (Exception ee)
            {
                System.Windows.MessageBox.Show(ee.Message);
            }
        }
        public void Stream1(string Way_to_File, string Text_to_stream)
        {
            using FileStream fstream = new FileStream(Way_to_File, FileMode.OpenOrCreate);
            byte[] array = Encoding.Default.GetBytes(Text_to_stream);

            fstream.Write(array, 0, array.Length);
        }

        public string Reading(string Way_to_read)
        {
            using FileStream fstream = File.OpenRead(Way_to_read);
            byte[] array = new byte[fstream.Length];

            fstream.Read(array, 0, array.Length);

            string textFromFile = Encoding.UTF8.GetString(array);
            return textFromFile;
        }

        public void ExistsDirectory()
        {
            string Way_txt;
            string Path = @"C:\Users\alexandr_user\Documents\MsCod";
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                Way_txt = String.Concat(Path, @"\File.txt");
                string letters = String.Concat(Path, @"\Letters.txt");

                FileInfo _txt = new FileInfo(Way_txt);
                FileInfo _letters_txt = new FileInfo(letters);
                _letters_txt.Create();
                _txt.Create();
            }
        }
    }
}
