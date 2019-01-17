using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.IO;

namespace Hash
{
    public enum Hashes
    {
        MD5, RIPEMD160, SHA1, SHA256, SHA384, SHA512
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            MainWindowLoad();
        }
        byte[] text;
        string file;
        FileStream fileStream;
        Hashes chooseHash;
        HashAlgorithm hash;
        

        public void MainWindowLoad()
        {
            for (int i = 0; i < Enum.GetValues(typeof(Hashes)).Length; i++)
            {
                comboBox1.Items.Add((Hashes)i);
            }
            comboBox1.Text = comboBox1.Items[0].ToString();
            chooseHash = (Hashes)0;
        }

        private async void calcHash_Click(object sender, RoutedEventArgs e)
        {
            string Result = await Task.Run(() => CalculateHash());
            resultLabel.Text = "Result:"+Result;
        }

        private string CalculateHash()
        {
            setHash();
            try
            {
                fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                fileStream.Position = 0;
                text = hash.ComputeHash(fileStream);
                fileStream.Close();
                StringBuilder hex = new StringBuilder(text.Length * 2);
                foreach (byte b in text)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                return hex.ToString();
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
            }
            return "";
        }
        
        private void setHash()
        {
            switch (chooseHash)
            {
                case (Hashes.MD5):
                    hash = MD5.Create();
                    break;
                case (Hashes.RIPEMD160):
                    hash = RIPEMD160.Create();
                    break;
                case (Hashes.SHA1):
                    hash = SHA1.Create();
                    break;
                case (Hashes.SHA256):
                    hash = SHA256.Create();
                    break;
                case (Hashes.SHA384):
                    hash = SHA384.Create();
                    break;
                case (Hashes.SHA512):
                    hash = SHA512.Create();
                    break;
            }
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            chooseHash = (Hashes)Enum.Parse(typeof(Hashes), comboBox1.Text);
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.DefaultExt = ".txt";
            openFileDialog1.Filter = "TXT Files (*.txt)|*.txt";
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                file = openFileDialog1.FileName;

                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    String line = sr.ReadToEnd();
                    textBox1.Text = line;
                }
            }
        }
    }
}
