using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_Criptografie
{
    public abstract class BazaCriptare
    {

        public string Key;

        public BazaCriptare(string s)
        {
            Id = s;
        }
        public string Id
        {
            get
            {
                return Key;
            }

            set
            {
                Key = value;
            }
        }


        /*
        protected string Key;

        public BazaCriptare(string Key)
        {
            this.Key = Key;
        }
        */
        public abstract string Crypt(string text);

        public abstract string Decrypt(string text);
    }
}
