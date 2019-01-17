using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_Criptografie
{
    public class RotN:BazaCriptare
    {
        public RotN(string Key)
            :base(Key)
        {

        }

        public override string Crypt(string text)
        {
            char[] abc = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            char[] ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] textsimplu = text.ToCharArray();
            char[] textcriptat = new char[text.Length];
            int x = Convert.ToInt32(Key);
            bool modificat;
            for (int i = 0; i < text.Length; i++)
            {
                modificat = false;
                for (int j = 0; j < abc.Length; j++)
                    if (abc[j] == text[i])
                    {
                        textcriptat[i] = abc[(j + x) % 26];
                        modificat = true; 
                    }

                for (int j = 0; j < ABC.Length; j++)
                    if (ABC[j] == text[i])
                    {
                        textcriptat[i] = ABC[(j + x) % 26];
                        modificat = true;
                    }

                if (modificat == false)
                    textcriptat[i] = text[i];
            }
            return String.Concat(textcriptat);
        }

        public override string Decrypt(string text)
        {
            char[] abc = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            char[] ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] textcriptat = text.ToCharArray();
            char[] textdecriptat = new char[textcriptat.Length];
            int x = Convert.ToInt32(Key);

            bool modificat;

            for (int i = 0; i < textcriptat.Length; i++)
            {
                modificat = false;
                for (int j = 0; j < abc.Length; j++)
                    if (abc[j] == textcriptat[i])
                    {
                        int c = j - x;
                        if (c < 0)
                            c += 26;
                        textdecriptat[i] = abc[c];
                        modificat = true;
                    }

                for (int j = 0; j < ABC.Length; j++)
                    if (ABC[j] == textcriptat[i])
                    {
                        int c = j - x;
                        if (c < 0)
                            c += 26;
                        textdecriptat[i] = ABC[c];
                        modificat = true;
                    }

                if (modificat == false)
                    textdecriptat[i] = textcriptat[i];
            }
            return  String.Concat(textdecriptat);
        }
    }
}
