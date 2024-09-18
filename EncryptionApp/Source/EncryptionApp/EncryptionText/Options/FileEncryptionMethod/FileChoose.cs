using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionApp.EncryptionText.Options.FileEncryptionMethod;



public partial class FileEncryption {
    AppUI AppUIFileOption;
    public FileEncryption(AppUI AppUIFileOption) {
        this.AppUIFileOption = AppUIFileOption;
    }

    public void OpenFileToEncryptionButton(Button OpenFileToEncryptionButton) {
        OpenFileToEncryptionButton.Text = "Encryption File";
        OpenFileToEncryptionButton.FlatStyle = FlatStyle.Flat;
        OpenFileToEncryptionButton.Location = new Point(150, 320);
        OpenFileToEncryptionButton.Width = 150;
        OpenFileToEncryptionButton.Height = 30;
        OpenFileToEncryptionButton.BackColor = Color.White;

        OpenFileToEncryptionButton.Click += new EventHandler(OpenFileToEncryptionButtonClicked);

        AppUIFileOption.Controls.Add(OpenFileToEncryptionButton);
    }


    private void OpenFileToEncryptionButtonClicked(object? sender, EventArgs eventArgs) {
        string MenuValue = AppUIFileOption.MenuSelectMethod.Text;
        using OpenFileDialog OpenFileToEncryption = new OpenFileDialog();

        OpenFileToEncryption.Filter = "All Files (*.*)|*.*";
       
        switch(MenuValue) {
            case "AES Encryption":

            try {
                OpenFileToEncryption.ShowDialog();

                string FilePath = OpenFileToEncryption.FileName;
                string FileContent = File.ReadAllText(FilePath);

                if(string.IsNullOrWhiteSpace(FileContent)) {
                MessageBox.Show("Your file is emty! No need encryption");
                return;
            }

                string KeyForDecode = GenerateKeyForDecode();
                byte[] TextWhenEncryption = EncryptionText(FileContent, KeyForDecode);

                string TextEncryption = Convert.ToBase64String(TextWhenEncryption);
            
                AppUIFileOption.LogResult.Text = $"Encryption File Done!\nPath to file : {FilePath}";
                File.WriteAllText(FilePath, $"{TextEncryption}");
                AppUIFileOption.ShowKeyForDecode.Text = $"{KeyForDecode}";
            
                MessageBox.Show($"Encryption file successfuly\nFile Path: {FilePath}");
        } catch {
            // Do nothing. Error handling has define in Encryption File method
        }
            break;

            case "Base64 Encryption":
            
            try {
                OpenFileToEncryption.ShowDialog();

                string FilePath = OpenFileToEncryption.FileName;
                string FileContent = File.ReadAllText(FilePath);

                if(string.IsNullOrWhiteSpace(FileContent)) {
                    MessageBox.Show("Your file is emty! No need encryption");
                    return;
                }
                
                string TextEncryption = Base64Encryption(FileContent);

                File.WriteAllText(FilePath, TextEncryption);
                MessageBox.Show($"Encrypted file successfully!\nPath to file:\n{FilePath}");

                AppUIFileOption.LogResult.Text = $"Decode Base64 to file as\n{FilePath}\nsuccessfuly!";
                AppUIFileOption.ShowKeyForDecode.Text = "";
            } catch {

            }

            break;

            case "Hash with SHA512":
               try {
                OpenFileToEncryption.ShowDialog();
                string FilePath = OpenFileToEncryption.FileName;
                string FileContent = File.ReadAllText(FilePath);

                if(string.IsNullOrWhiteSpace(FileContent)) {
                    MessageBox.Show("Your file is emty! No need hash");
                    return;
                }

                string FileSHA512 = HashSHA512(FileContent);
                File.WriteAllText(FilePath, FileSHA512);

                MessageBox.Show($"Hash file as:\n{FilePath}\nsuccessfully");
                AppUIFileOption.LogResult.Text = $"Successfully HASH file as\n{FilePath}";
                AppUIFileOption.ShowKeyForDecode.Text = "";
               } catch {

               }
            break;
        }
    }
    

    private string GenerateKeyForDecode() {
        using (Aes aes = Aes.Create()) {
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }
    }

   
    private byte[] EncryptionText(string TextWantEncryption, string Key) {
        using (Aes aesAlg = Aes.Create()) {
            aesAlg.Key = Convert.FromBase64String(Key); 
            aesAlg.IV = new byte[16]; 

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream()) {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                        swEncrypt.Write(TextWantEncryption);
                    }
                    return msEncrypt.ToArray();
                }
            }
        }
    }

    private string Base64Encryption(string OriginalText) {
        byte[] OriginalTextBytes = System.Text.Encoding.UTF8.GetBytes(OriginalText);

        string Base64Text = Convert.ToBase64String(OriginalTextBytes);

        return Base64Text;
    }

    private string HashSHA512(string TextToHash) {
        byte[]TextBytes = Encoding.UTF8.GetBytes(TextToHash);

        using SHA512 Hash512 = SHA512.Create();
        byte[] HashStringBytes = Hash512.ComputeHash(TextBytes);

        StringBuilder HashString = new StringBuilder();

        foreach(byte Bytes in HashStringBytes) {
            HashString.Append(Bytes.ToString("x2"));
        }

        return HashString.ToString();
    }
}