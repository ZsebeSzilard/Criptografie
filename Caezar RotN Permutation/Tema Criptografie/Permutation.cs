using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_Criptografie
{
    public class Permutation:BazaCriptare
    {
        public Permutation()
            : base(MakeKey())
        {

        }
        public static string MakeKey()
        {
            char[] abc1 = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            int lenghtofnewabc = abc1.Length;
            char[] abc2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var numbersList1 = abc1.ToList();
            var numbersList2 = abc2.ToList();
            char[] newabc1 = new char[abc1.Length];
            char[] newabc2 = new char[abc2.Length];

            Random rnd = new Random();
            for (int i = 0; i < abc1.Length; i++)
            {
                int x = rnd.Next(0, lenghtofnewabc);

                newabc1[i] = numbersList1[x];
                numbersList1.RemoveAt(x);

                newabc2[i] = numbersList2[x];
                numbersList2.RemoveAt(x);

                lenghtofnewabc--;
            }

            string a = new string(newabc1);
            string b = new string(newabc2);
            string c = a+b;
            return c;
        }
        public override string Crypt(string text)
        {
            char[] abc1 = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            char[] abc2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] newabc1 = new char[abc1.Length];
            char[] newabc2 = new char[abc2.Length];
            for (int i = 0; i < abc1.Length; i++)
                newabc1[i] = Key[i];
            for (int i = abc1.Length; i < abc1.Length+abc2.Length; i++)
                newabc2[i-26] = Key[i];

            char[] plaintext = text.ToCharArray();
            char[] textcriptat = new char[text.Length];
            

            for (int i = 0; i < plaintext.Length; i++)
            {
                textcriptat[i] = plaintext[i];
                for (int j = 0; j < abc1.Length; j++)
                    if (plaintext[i] == abc1[j])
                        textcriptat[i] = newabc1[j];
                for (int j = 0; j < abc2.Length; j++)
                    if (plaintext[i] == abc2[j])
                        textcriptat[i] = newabc2[j];

            }

            string a = new string(textcriptat);
            return a;
        }
        public override string Decrypt(string text)
        {
            char[] abc1 = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            char[] abc2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] newabc1 = new char[abc1.Length];
            char[] newabc2 = new char[abc2.Length];
            for (int i = 0; i < abc1.Length; i++)
                newabc1[i] = Key[i];
            for (int i = abc1.Length; i < abc1.Length + abc2.Length; i++)
                newabc2[i - 26] = Key[i];

            char[] textcriptat = text.ToCharArray();
            char[] textdecriptat = new char[text.Length];



            for (int i = 0; i < textcriptat.Length; i++)
            {
                textdecriptat[i] = textcriptat[i];
                for (int j = 0; j < abc1.Length; j++)
                    if (textcriptat[i] == newabc1[j])
                        textdecriptat[i] = abc1[j];
                for (int j = 0; j < abc1.Length; j++)
                    if (textcriptat[i] == newabc2[j])
                        textdecriptat[i] = abc2[j];

            }
            string b = new string(textdecriptat);
            
            return b;
        }

    }


}
