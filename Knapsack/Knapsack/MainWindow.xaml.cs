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
using System.IO;
namespace Knapsack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string file;
        static int q;
        static int r;
        static int[] w = { 2, 7, 11, 21, 42, 89, 180, 354 };
        static int[] beta;
        static bool keys_are_generated = false;
        string plainText;
        int[] encoded;


        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Button_Crypt(object sender, RoutedEventArgs e)
        {
            plainText = textBox1.Text;
            if (keys_are_generated && plainText != "")
            {
                textBox2.Text = "";
                encoded = Encrypt(plainText, beta);
                int count = 0;

                for (int i = 0; i < encoded.Length; i++)
                {
                    //Console.Write("{0, 5}", encoded[i]);
                    textBox2.Text += encoded[i];
                    count++;
                    if (count == 16)
                    {
                        count = 0;
                        //Console.WriteLine();
                        textBox2.Text += '\n';
                    }
                }

                if (count > 0) //Console.WriteLine();
                    textBox2.Text += '\n';
            }
            else if (!keys_are_generated && plainText == "") {
                MessageBox.Show("Please Browse for a text and generate the keys");
            }else if(plainText == "")
            {
                MessageBox.Show("Please Browse for a text!");
            }else if (!keys_are_generated)
            {
                MessageBox.Show("Please generate the keys!");
            }
        }
        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {
            if (keys_are_generated&&encoded!=null)
            {
                string decoded = Decrypt(encoded, w, q, r);
                textBox3.Text = decoded;
            }
            else if(!keys_are_generated && encoded == null)
                MessageBox.Show("Please crypt a text and generate the keys!");
            else if(!keys_are_generated)
                MessageBox.Show("Please generate the keys!");
            else if(encoded==null)
                MessageBox.Show("Please crypt a text!");
        }
        private void Button_Keys(object sender, RoutedEventArgs e)
        {
            q = 881;
            r = 588;
            beta = GetPublicKey(w, q, r);
            keys_are_generated = true;
            label1.Text = "Keys generated";
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
            plainText = textBox1.Text;
        }
        static int[] GetPublicKey(int[] w, int q, int r)
        {
            int[] beta = new int[w.Length];
            for (int i = 0; i < w.Length; i++) beta[i] = w[i] * r % q;
            return beta;
        }

        static int[] Encrypt(string plainText, int[] beta)
        {
            if (String.IsNullOrEmpty(plainText)) return null;
            int[] encoded = new int[plainText.Length];

            for (int i = 0; i < encoded.Length; i++)
            {
                string bin = ConvertToBinary(plainText[i]);
                int sum = 0;
                for (int j = 0; j < 8; j++) sum += (bin[j] - 48) * beta[j];
                encoded[i] = sum;
            }

            return encoded;
        }

        static string Decrypt(int[] encoded, int[] w, int q, int r)
        {
            if (encoded == null || encoded.Length == 0) return null;
            char[] chars = new char[encoded.Length];
            int mir = ModInverse(r, q);
            if (mir == 0)
            {
                Console.WriteLine("Modular inverse does not exist. Decryption aborted");
                return null;
            }

            for (int i = 0; i < encoded.Length; i++)
            {
                char[] bin = new char[8];
                for (int j = 0; j < 8; j++) bin[j] = '0';
                int temp = encoded[i] * mir % q;

                while (temp > 0)
                {
                    int index = 7;

                    for (int j = 1; j < w.Length; j++)
                    {
                        if (w[j] > temp)
                        {
                            index = j - 1;
                            break;
                        }
                    }

                    bin[index] = '1';
                    temp -= w[index];
                }

                chars[i] = ConvertFromBinary(new string(bin));
            }

            return new string(chars);
        }

        static string ConvertToBinary(char c)
        {
            return Convert.ToString((int)c, 2).PadLeft(8, '0');
        }

        static char ConvertFromBinary(string bin)
        {
            return (char)Convert.ToInt32(bin, 2);
        }


        static int ModInverse(int r, int q)
        {
            int i = q, v = 0, d = 1;

            while (r > 0)
            {
                int t = i / r, x = r;
                r = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }

            v %= q;
            if (v < 0) v = (v + q) % q;
            return v;
        }
    }
}
