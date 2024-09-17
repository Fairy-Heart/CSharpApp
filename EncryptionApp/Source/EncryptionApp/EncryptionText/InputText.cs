using System;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;

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
        string TextNeedEncryption = EncryptionApp.EnterTextBox.Text;
        
        if(string.IsNullOrWhiteSpace(TextNeedEncryption)) {
            MessageBox.Show("You need enter some text to decode/encryption!");
            return;
        }
        KeyForDecode = GenerateKeyForDecode();
        byte[] TextWhenEncryption = EncryptionText(TextNeedEncryption, KeyForDecode);

        TextEncryption = Convert.ToBase64String(TextWhenEncryption);
        
       
        EncryptionApp.LogResult.Text = $"{TextEncryption}";
        EncryptionApp.ShowKeyForDecode.Text = $"{KeyForDecode}";
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
    
}