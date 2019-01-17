using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Tema_Criptografie
{
    class Program
    {
        static void Main(string[] args)
        {
            string textintrare;
            using (StreamReader sr = new StreamReader(@"..\\..\\TextFile1.txt"))
            {
                textintrare = sr.ReadLine();
            }
            char[] text = textintrare.ToCharArray();

            
            string x="5";
            RotN a=new RotN(x);
            string criptat= a.Crypt(textintrare);
            string decriptat= a.Decrypt(a.Crypt(textintrare));
            Console.WriteLine(criptat);
            Console.WriteLine(decriptat);
            using (StreamWriter writer = new StreamWriter(@"RotnCriptat.txt"))
            {
                for (int i = 0; i < criptat.Length; i++)
                    writer.Write(criptat[i]);
            }
            using (StreamWriter writer = new StreamWriter(@"RotnDecriptat.txt"))
            {
                for (int i = 0; i < decriptat.Length; i++)
                    writer.Write(decriptat[i]);
            }
            Console.WriteLine("---------------------------------");

            Caesar b = new Caesar();
            criptat = b.Crypt(textintrare);
            decriptat = b.Decrypt(b.Crypt(textintrare));
            Console.WriteLine(criptat);
            Console.WriteLine(decriptat);
            using (StreamWriter writer = new StreamWriter(@"CaesarCriptat.txt"))
            {
                for (int i = 0; i < criptat.Length; i++)
                    writer.Write(criptat[i]);
            }
            using (StreamWriter writer = new StreamWriter(@"CaesarDecriptat.txt"))
            {
                for (int i = 0; i < decriptat.Length; i++)
                    writer.Write(decriptat[i]);
            }

            Console.WriteLine("---------------------------------");

            Permutation c = new Permutation();
            criptat = c.Crypt(textintrare);
            decriptat = c.Decrypt(c.Crypt(textintrare));
            Console.WriteLine(criptat);
            Console.WriteLine(decriptat);
            using (StreamWriter writer = new StreamWriter(@"PermutationCriptat.txt"))
            {
                for (int i = 0; i < criptat.Length; i++)
                    writer.Write(criptat[i]);
            }
            using (StreamWriter writer = new StreamWriter(@"PermutationDecriptat.txt"))
            {
                for (int i = 0; i < decriptat.Length; i++)
                    writer.Write(decriptat[i]);
            }
            Console.WriteLine("---------------------------------");
            Console.ReadLine();
        }
    }
}
