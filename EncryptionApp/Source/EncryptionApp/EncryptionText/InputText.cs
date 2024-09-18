using System;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionApp.EncryptionText;


public partial class InputText {
    private readonly AppUI EncryptionApp;

    public string? TextEncryption;

    public string? KeyForDecode;

    public InputText(AppUI EncryptionApp) {
        this.EncryptionApp = EncryptionApp;

    }

    public void SetInputText(RichTextBox InputText, Label Description) {
        InputText.Location = new Point(40, 55);
        InputText.Size = new Size(500, 250);
        InputText.AcceptsTab = true;

        Description.Location = new Point(40, 35);
        Description.Text = "Enter Text To Decode/Encryption:";
        Description.ForeColor = Color.White;
        Description.Width = 200;

        EncryptionApp.Controls.Add(InputText);
        EncryptionApp.Controls.Add(Description);
    }


    public void SendDataForEncryptionTextButton(Button SendButton) {
        SendButton.Text = "Encryption Text";
        SendButton.FlatStyle = FlatStyle.Flat;
        SendButton.Location = new Point(40, 320);
        SendButton.BackColor = Color.White;
        SendButton.Width = 100;
        SendButton.Height = 30;

        SendButton.Click += new EventHandler(SendDataForEncryptionText);

        EncryptionApp.Controls.Add(SendButton);
    }

    private void SendDataForEncryptionText(object? sender, EventArgs eventArgs) {
        string MethodValue = EncryptionApp.MenuSelectMethod.Text;
        string TextNeedEncryption = EncryptionApp.EnterTextBox.Text;
        
        if(string.IsNullOrWhiteSpace(TextNeedEncryption)) {
            MessageBox.Show("You need enter some text to decode/encryption!");
            return;
        }
        switch(MethodValue) {
            case "AES Encryption":
            KeyForDecode = GenerateKeyForDecode();
            byte[] TextWhenEncryption = EncryptionText(TextNeedEncryption, KeyForDecode);

            TextEncryption = Convert.ToBase64String(TextWhenEncryption);
        
       
            EncryptionApp.LogResult.Text = $"{TextEncryption}";
            EncryptionApp.ShowKeyForDecode.Text = $"{KeyForDecode}";
            break;

            case "Base64 Encryption":
            string EncryptedText = Base64Encryption(TextNeedEncryption);
            EncryptionApp.LogResult.Text = $"{EncryptedText}";
            EncryptionApp.ShowKeyForDecode.Text = "";
            break;

            case "Hash with SHA512":
               try {

                if(string.IsNullOrWhiteSpace(TextNeedEncryption)) {
                    MessageBox.Show("Your file is emty! No need hash");
                    return;
                }

                string TextSHA512 = HashSHA512(TextNeedEncryption);

                EncryptionApp.LogResult.Text = $"{TextSHA512}";
                MessageBox.Show($"Hash with SHA512 method successfully!");
                EncryptionApp.ShowKeyForDecode.Text = "";
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