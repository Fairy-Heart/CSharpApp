using System;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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
        string MethodValue = AppUIDecodeText.MenuSelectMethod.Text;
        string TextEncryption = AppUIDecodeText.EnterTextBox.Text;

        string KeyDecode = AppUIDecodeText.KeyDecode.Text;

         if(MethodValue == "Hash with SHA512") {
            MessageBox.Show("Can't decode SHA512 HASH!");
            return;
        }

        if(string.IsNullOrWhiteSpace(TextEncryption)) {
            MessageBox.Show("Text encryption is emty! Can't decode");
            return;
        }

        if(MethodValue == "AES Encryption" && string.IsNullOrWhiteSpace(KeyDecode)) {
            MessageBox.Show("AES need KEY to decode! Please input your KEY");
            return;
        }

       

        switch(MethodValue){
            case "AES Encryption":
            try {
              string OriginalText = DecryptText(TextEncryption, KeyDecode);

              AppUIDecodeText.LogResult.Text = $"{OriginalText}";
              MessageBox.Show($"Decode with method {MethodValue} successfuly!");
            } catch {
            // Do nothing. Error handling is define in DecryptText method :D
            }
            break;

            case "Base64 Encryption":
            try {
            string IsBase64 = @"^[a-zA-Z0-9+/]*={0,2}$";

            string Base64Code = TextEncryption;

            if(Regex.IsMatch(Base64Code, IsBase64)) {
                string TextBase64Decode = Base64Decode(Base64Code);

                AppUIDecodeText.LogResult.Text = $"{TextBase64Decode}";
                AppUIDecodeText.ShowKeyForDecode.Text = "";
                MessageBox.Show($"Decode with method {MethodValue} successfuly!");
                return;
            }

            } catch {
               // Do nothing. Error handling set to Base64Decode method.
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
        MessageBox.Show("KEY or Text Encryption is incorrect!");
        throw;
        }
    }


    private string Base64Decode(string Base64Text) {
        try {
        byte[] Base64TextBytes = Convert.FromBase64String(Base64Text);

        string OrignialText = System.Text.Encoding.UTF8.GetString(Base64TextBytes);
        return OrignialText;
        } catch {
            MessageBox.Show("Invalid Base64 encryption!");
            throw;
        }
    }
}