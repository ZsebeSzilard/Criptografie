using System.Windows.Forms;

namespace RSA
{
    class MainWindow
    {
        private const int WIDTH = 580, HEIGHT = 530, LEFT = 300, TOP = 300;
        private Form form;
        private TextBox original;
        private TextBox ciphered;
        private TextBox publicKey;
        private TextBox privateKey;
        private TextBox decrypted;
        private Button fileDialog;
        private Button encrypt;
        private Button decrypt;
        private Button exit;
        private RSA rsa;
        private FileOperation fo;

        public MainWindow()
        {
            rsa = new RSA();
            fo = new FileOperation();

            int col1Left = 10;
            int col2Left = 350;
            int colTop = 10;

            form = new Form
            {
                Text = "RSA",
                Width = WIDTH,
                Height = HEIGHT,
                StartPosition = FormStartPosition.CenterScreen,
                Left = LEFT,
                Top = TOP,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            Label label1 = new Label { Text = "Original Text", Top = colTop, Left = col1Left };
            Label label2 = new Label { Text = "Ciphered Text", Top = colTop + 150, Left = col1Left };
            Label label5 = new Label { Text = "Public Key", Top = colTop + 120, Left = col2Left };
            Label label6 = new Label { Text = "Private Key", Top = colTop + 180, Left = col2Left };
            Label label7 = new Label { Text = "Select a file using the button below", Top = colTop + 250, Left = col2Left, Width = 200 };
            Label label8 = new Label { Text = "Decrypted", Top = colTop+300, Left = col1Left };

            original = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical, Top = 40, Left = col1Left, Height = 100, Width = 300 };
            ciphered = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical, Top = 190, Left = col1Left, Height = 100, Width = 300 };
            decrypted = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical, Top = 340, Left = col1Left, Height = 100, Width = 300 };

            publicKey = new TextBox { Top = 160, Left = col2Left, Width = 200 };
            privateKey = new TextBox { Top = 220, Left = col2Left, Width = 200 };

            

            fileDialog = new Button { Text = "Browse", Top = 290, Left = col2Left, Width = 200 };
            fileDialog.Click += FileDialogClicked;
            encrypt = new Button { Text = "Encrypt", Top = 320, Left = col2Left, Width = 200 };
            encrypt.Click += EncryptClicked;
            decrypt = new Button { Text = "Decrypt", Top = 350, Left = col2Left, Width = 200 };
            decrypt.Click += DecryptClicked;
            exit = new Button { Text = "Exit", Top = 380, Left = col2Left, Width = 200 };
            exit.Click += ExitClicked;

            form.Controls.Add(label1);
            form.Controls.Add(label2);
            form.Controls.Add(label5);
            form.Controls.Add(label6);
            form.Controls.Add(label7);
            form.Controls.Add(label8);
            form.Controls.Add(original);
            form.Controls.Add(ciphered);
            form.Controls.Add(publicKey);
            form.Controls.Add(privateKey);
            form.Controls.Add(decrypted);
            form.Controls.Add(fileDialog);
            form.Controls.Add(encrypt);
            form.Controls.Add(decrypt);
            form.Controls.Add(exit);
            form.ShowDialog();
        }

        private void FileDialogClicked(object sender, System.EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            string fileName;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = fileDialog.FileName;

                if (!fileName.EndsWith(".txt") && !fileName.EndsWith(".TXT"))
                {
                    MessageBox.Show("Please select text file only");
                }

                else
                {
                    fo.FileName = fileName; 
                    fo.Read(); 
                    original.Text = fo.Content; 
                }
            }
        }

        private void EncryptClicked(object sender, System.EventArgs e)
        {
            string t = original.Text; 
            string enc = rsa.Encrypt(t);
            ciphered.Text = enc;
            publicKey.Text = "( " + rsa.GetN + ", " + rsa.GetE + " )";
        }

        private void DecryptClicked(object sender, System.EventArgs e)
        {
            string t = ciphered.Text; 
            string dec = rsa.Decrypt(t);
            original.Text = dec;
            privateKey.Text = "( " + rsa.GetN + ", " + rsa.GetD + " )";
            decrypted.Text = dec;
        }

        private void ExitClicked(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}