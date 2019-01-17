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

namespace DES_AES_RC2_Rijndael_TripleDes
{
    public enum Alg
    {
        DES, Aes, RC2, Rijndael, TripleDes
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowLoad();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        static Random rnd = new Random();
        byte[] Cheie;
        byte[] IV;

        private void MainWindowLoad()
        {
            for (int i = 0; i < Enum.GetValues(typeof(Alg)).Length; i++)
                comboBox1.Items.Add((Alg)i);
            comboBox1.Text = comboBox1.Items[0].ToString();

            for (int i = 1; i <= Enum.GetValues(typeof(CipherMode)).Length; i++)
                comboBox2.Items.Add((CipherMode)i);
            comboBox2.Text = comboBox2.Items[0].ToString();

            for (int i = 1; i <= Enum.GetValues(typeof(PaddingMode)).Length; i++)
                comboBox3.Items.Add((PaddingMode)i);
            comboBox3.Text = comboBox3.Items[0].ToString();

            Cheie = new byte[16];
            IV = new byte[16];
        }
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Alg Algoritm = (Alg)Enum.Parse(typeof(Alg), comboBox1.Text);
            CipherMode Cipher = (CipherMode)Enum.Parse(typeof(CipherMode), comboBox2.Text);
            PaddingMode Padding = (PaddingMode)Enum.Parse(typeof(PaddingMode), comboBox3.Text);
            Criptare(textBox1.Text, textBox2.Text, Cheie, IV, Algoritm, Cipher, Padding, false);
        }
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Alg Algoritm = (Alg)Enum.Parse(typeof(Alg), comboBox1.Text);
            CipherMode Cipher = (CipherMode)Enum.Parse(typeof(CipherMode), comboBox2.Text);
            PaddingMode Padding = (PaddingMode)Enum.Parse(typeof(PaddingMode), comboBox3.Text);
            Criptare(textBox1.Text, textBox2.Text, Cheie, IV, Algoritm, Cipher, Padding, true);
        }
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.DefaultExt = ".txt";
            openFileDialog1.Filter = "TXT Files (*.txt)|*.txt";
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                string strfilename = openFileDialog1.FileName;
                FileStream f = new FileStream(strfilename, FileMode.Open, FileAccess.Read);
                string buffer = File.ReadAllText(strfilename);
                char[] Separator = { ' ', ',', '.' };
                string[] bytes = buffer.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
                IV = new byte[bytes.GetLength(0)];
                for (int i = 0; i < IV.Length; i++)
                    IV[i] = byte.Parse(bytes[i]);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.DefaultExt = ".txt";
            openFileDialog1.Filter = "TXT Files (*.txt)|*.txt";
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                string strfilename = openFileDialog1.FileName;
                FileStream f = new FileStream(strfilename, FileMode.Open, FileAccess.Read);
                string buffer = File.ReadAllText(strfilename);
                char[] Separator = { ' ', ',', '.' };
                string[] bytes = buffer.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
                Cheie = new byte[bytes.GetLength(0)];
                for (int i = 0; i < Cheie.Length; i++)
                {
                    Cheie[i] = byte.Parse(bytes[i]);
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.DefaultExt = ".txt";
            openFileDialog1.Filter = "TXT Files (*.txt)|*.txt";
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                string strfilename = openFileDialog1.FileName;
                textBox2.Text = strfilename;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.DefaultExt = ".txt";
            openFileDialog1.Filter = "TXT Files (*.txt)|*.txt";
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                string strfilename = openFileDialog1.FileName;
                textBox1.Text = strfilename;
            }
        }


        private static void Criptare(String inName, String outName, byte[] EnKey, byte[] EnIV, Alg SymAlg, CipherMode Cm, PaddingMode Pm, bool crypt)
        {
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            byte[] bin = new byte[100];
            long rdlen = 0;
            long totlen = fin.Length;
            int len;
            try
            {
                SymmetricAlgorithm rijn = SymmetricAlgorithm.Create(SymAlg.ToString());
                rijn.Mode = Cm;
                rijn.Padding = Pm;
                //MessageBox.Show(SymAlg.ToString() + Cm.ToString() + Pm.ToString());
                CryptoStream cryptStream;
                if (crypt == true)
                {
                    cryptStream = new CryptoStream(fout, rijn.CreateEncryptor(EnKey, EnIV), CryptoStreamMode.Write);
                }
                else
                {
                    cryptStream = new CryptoStream(fout, rijn.CreateDecryptor(EnKey, EnIV), CryptoStreamMode.Write);
                }
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, 100);
                    cryptStream.Write(bin, 0, len);
                    rdlen = rdlen + len;
                }

                cryptStream.Close();
                fout.Close();
                fin.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                fout.Close();
                fin.Close();
            }
        }
    }
}