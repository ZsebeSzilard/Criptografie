using System;
using System.Numerics;
using System.Text;

namespace RSA
{
    class RSA
    {
        private int[] cipheredText;
        private int p, q, n, phi, d, e;
        private PrimeNumbers pn;

        public RSA()
        {
            pn = new PrimeNumbers();
        }

        private void SetParameters()
        {
            p = pn.RandomPrime();
            q = pn.RandomPrime();

            if (p > q)
            {
                int t = q;
                q = p;
                p = t;
            }

            n = p * q;
            phi = (p - 1) * (q - 1);
        }

        private bool IsCoPrime(int a, int b)
        {
            int g = GCD(a, b);
            return (g == 1) ? true : false;
        }

        private int GCD(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }

            return GCD(b % a, a);
        }

        private int E()
        {
            for (int i = 2; i < phi; i++)
            {
                if (IsCoPrime(i, phi))
                {
                    return i;
                }
            }

            return -1;
        }

        private int D(int phi, int e)
        {
            int r = 1;

            while ((e * r - 1) % phi != 0)
            {
                r++;
            }

            d = r;
            return r;
        }

        private byte[] StringToByte(string input)
        {
            int s = input.Length;
            byte[] r = new byte[s];
            char temp;
            byte t;

            for (int i = 0; i < s; i++)
            {
                temp = input[i];
                t = (byte)temp;

                if (Char.IsDigit(temp)) { t = (byte)(t - 48); }

                else if (temp == ' ') { t = (byte)199; }

                else if (temp == '\n') { t = (byte)200; }

                else if (t == 13) { t = (byte)201; }

                else { t = (byte)(t - 55); }

                r[i] = t;
            }

            return r;
        }

        private string ByteToString(byte[] b)
        {
            int s = b.Length;
            StringBuilder sb = new StringBuilder();
            byte temp;

            for (int i = 0; i < s; i++)
            {
                temp = b[i];

                if (temp >= 0 && temp <= 9) { temp = (byte)(temp + 48); }

                else if (temp == 199) { temp = (byte)32; }

                else if (temp == 200) { temp = (byte)10; }

                else if (temp == 201) { temp = (byte)13; }

                else { temp = (byte)(temp + 55); }

                sb.Append((char)temp);
            }

            return sb.ToString();
        }

        private string IntToString(int[] b)
        {
            int l = b.Length;
            byte[] m = new byte[l];

            for (int i = 0; i < l; i++)
            {
                m[i] = (byte)b[i];
            }

            return Encoding.UTF8.GetString(m);
        }

        private int[] CipheredText(byte[] a, int e, int n)
        {
            int length = a.Length, temp;
            int[] c = new int[length];

            for (int i = 0; i < length; i++)
            {
                temp = a[i];

                if (temp >= 199 && temp <= 201)
                {
                    c[i] = temp;
                    continue;
                }

                temp = (int)Math.Pow(temp, e);
                temp %= n;
                c[i] = temp;
            }

            return c;
        }

        private byte[] DecipheredText(int[] a, int d, int n)
        {
            int l = a.Length;
            byte[] r = new byte[l];

            for (int i = 0; i < a.Length; i++)
            {
                int temp = a[i];

                if (temp == 199 || temp == 200 || temp == 201)
                {
                    r[i] = (byte)temp;
                    continue;
                }

                BigInteger bi = temp;
                bi = BigInteger.ModPow(bi, d, n);
                string tempString = bi.ToString();
                int g = Int32.Parse(tempString);
                r[i] = (byte)g;
            }

            return r;
        }

        public string Encrypt(string message)
        {
            SetParameters();
            byte[] arr = StringToByte(message);
            e = E();
            d = D(phi, e);
            cipheredText = CipheredText(arr, e, n);
            StringBuilder builder = new StringBuilder();
            int len = cipheredText.Length;

            for (int j = 0; j < len; j++)
            {
                builder.Append(cipheredText[j]);
            }

            return builder.ToString();
        }

        public string Decrypt(string message)
        {
            char[] temp = message.ToCharArray();
            byte[] plainText = DecipheredText(cipheredText, d, n);
            string decipheredText = ByteToString(plainText);
            return decipheredText;
        }

        public int GetN
        { get { return n; } set { n = value; } }

        public int GetD
        { get { return d; } set { d = value; } }

        public int GetE
        { get { return e; } set { e = value; } }
    }
}