using System;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace EncryptionApp.EncryptionText.Options.FileEncryptionMethod;


public partial class DecodeFile {
    private readonly AppUI AppUIDecodeFile;

    public DecodeFile(AppUI AppUIDecodeFile) {
        this.AppUIDecodeFile = AppUIDecodeFile;
    }

    public void DecodeFileButton(Button DecodeFileButton) {
        DecodeFileButton.Text = "Decode File";
        DecodeFileButton.FlatStyle = FlatStyle.Flat;
        DecodeFileButton.Location = new Point(825, 320);
        DecodeFileButton.Width = 150;
        DecodeFileButton.Height = 30;
        DecodeFileButton.BackColor = Color.White;
        DecodeFileButton.Click += new EventHandler(DecodeFileButtonClicked);

        AppUIDecodeFile.Controls.Add(DecodeFileButton);
    }

    private void DecodeFileButtonClicked(object? sender, EventArgs eventArgs) {
        string MenuValue = AppUIDecodeFile.MenuSelectMethod.Text;
        using OpenFileDialog ChooseForDecodeFile = new OpenFileDialog();

        ChooseForDecodeFile.Filter = "All files (*.*)|*.*";
        string KeyForDecode  = AppUIDecodeFile.KeyDecode.Text;
        if(string.IsNullOrWhiteSpace(KeyForDecode) && MenuValue == "AES Encryption") {
            MessageBox.Show("Please input KEY decode first");
            return;
        }

        if(MenuValue == "Hash with SHA512") {
            MessageBox.Show("Can't decode SHA512 HASH!");
            return;
        }

       switch(MenuValue) {
        case "AES Encryption":
         try {
            ChooseForDecodeFile.ShowDialog();
            string EncryptionFilePath = ChooseForDecodeFile.FileName;
            string EncryptionContent = File.ReadAllText(EncryptionFilePath);

            string OriginalText = DecryptText(EncryptionContent, KeyForDecode);

            File.WriteAllText(EncryptionFilePath, OriginalText);

            AppUIDecodeFile.LogResult.Text = $"Decode file successfuly!\nPath to file: {EncryptionFilePath}";

            MessageBox.Show($"Decode file successfuly!\nPath to file:\n{EncryptionFilePath}");

            AppUIDecodeFile.ShowKeyForDecode.Text = "";

        } catch {
            // Do nothing. Set to DecrypText method.
        }
        break;

        case "Base64 Encryption":
        try {
            ChooseForDecodeFile.ShowDialog();
            string Base64EncryptedPath = ChooseForDecodeFile.FileName;
            string Base64Content = File.ReadAllText(Base64EncryptedPath);

            string Base64DecodeText = Base64Decode(Base64Content);

            File.WriteAllText(Base64EncryptedPath, Base64DecodeText);
            
            AppUIDecodeFile.LogResult.Text = $"Decode file successfuly\nPath to file: {Base64EncryptedPath}";

            MessageBox.Show($"Decode file successfuly!\nPath to file:\n{Base64EncryptedPath}");

            AppUIDecodeFile.ShowKeyForDecode.Text = "";

        } catch {
            // Do nothing. Set to Base64Decode method
        }
        break;
       }
    }

    private string DecryptText(string EncryptionText, string Key) {
        try {
        
        byte[] EncryptionTextBytes = Convert.FromBase64String(EncryptionText);
        using (Aes aesAlg = Aes.Create()) {
        aesAlg.Key = Convert.FromBase64String(Key);
        aesAlg.IV = new byte[16]; 

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using (MemoryStream msDecrypt = new MemoryStream(EncryptionTextBytes)) {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt)) {
                    return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    } catch {
        MessageBox.Show("KEY or text in file encryption is incorrect!");
        AppUIDecodeFile.LogResult.Text = "";
        AppUIDecodeFile.ShowKeyForDecode.Text = "";
        throw;
        }
    }


    private string Base64Decode(string Base64Text) {
        try {
        byte[] Base64TextBytes = Convert.FromBase64String(Base64Text);

        string OrignialText = System.Text.Encoding.UTF8.GetString(Base64TextBytes);
        return OrignialText;
        } catch {
            MessageBox.Show("Invalid Base64 encryption in this file!");
            throw;
        }
    }

}