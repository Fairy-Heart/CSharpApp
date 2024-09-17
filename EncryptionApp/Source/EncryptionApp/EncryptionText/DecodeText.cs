using System;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;

namespace EncryptionApp.EncryptionText;

public partial class DecodeText {
    private readonly AppUI AppUIDecodeText;
    public DecodeText(AppUI AppUIDecodeText) {
        this.AppUIDecodeText = AppUIDecodeText;
    }

    public void InputKeyToDecodeText(RichTextBox InputKeyBox, Label Description) {
            Description.Text = "Input key for decode this text:";
            Description.ForeColor = Color.White;
            Description.Width = 200;
            Description.Location = new Point(40, 395);

            InputKeyBox.Location = new Point(40, 420);
            InputKeyBox.Size = new Size (500, 100);

            AppUIDecodeText.Controls.Add(Description);
            AppUIDecodeText.Controls.Add(InputKeyBox);
    }

    public void InputKeyToDecodeTextButton(Button InputKeyToDecodeTextButton) {
        InputKeyToDecodeTextButton.Text = "Decode Text Encryption";
        InputKeyToDecodeTextButton.BackColor = Color.White;
        InputKeyToDecodeTextButton.Width = 200;
        InputKeyToDecodeTextButton.Height = 30;
        InputKeyToDecodeTextButton.Location = new Point(40, 530);
        InputKeyToDecodeTextButton.Click += new EventHandler(InputKeyToDecodeTextButtonClicked);

        AppUIDecodeText.Controls.Add(InputKeyToDecodeTextButton);
    }

    private void InputKeyToDecodeTextButtonClicked(object? sender, EventArgs eventArgs) {
        string TextEncryption = AppUIDecodeText.EnterTextBox.Text;

        string KeyDecode = AppUIDecodeText.KeyDecode.Text;

        if(string.IsNullOrWhiteSpace(TextEncryption) || string.IsNullOrWhiteSpace(KeyDecode)) {
            MessageBox.Show("Text encryption or KEY decode is emty! Can't decode");
            return;
        }

        try {
        string OriginalText = DecryptText(TextEncryption, KeyDecode);

        AppUIDecodeText.LogResult.Text = $"{OriginalText}";
        } catch {
            // Do nothing. Error handling is define in DecryptText method :D
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
        MessageBox.Show("KEY or Text Encryption is incorrect!");
        throw;
        }
    }
}