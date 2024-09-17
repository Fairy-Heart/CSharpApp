using System;
using System.Windows.Forms;

namespace EncryptionApp.EncryptionText;

public partial class ShowKey {
    private readonly AppUI AppUIShowKey;

    public ShowKey(AppUI AppUIShowKey) {
        this.AppUIShowKey = AppUIShowKey;
    }

    public void ShowKeyLog(RichTextBox ShowKeyLog, Label Description) {
            Description.Text = "Key for decode this text:";
            Description.ForeColor = Color.White;
            Description.Width = 200;
            Description.Location = new Point(600, 400);

            ShowKeyLog.Location = new Point(600, 420);
            ShowKeyLog.Size = new Size(500, 100);
            ShowKeyLog.ReadOnly = true;

            AppUIShowKey.Controls.Add(ShowKeyLog);
            AppUIShowKey.Controls.Add(Description);
    }

    public void CopyKeyLog(Button CopyKeyLogButton) {
        CopyKeyLogButton.Text = "Copy Key";
        CopyKeyLogButton.FlatStyle = FlatStyle.Flat;
        CopyKeyLogButton.BackColor = Color.White;
        CopyKeyLogButton.Width = 100;
        CopyKeyLogButton.Height = 30;

        CopyKeyLogButton.Location = new Point(600, 530);
        CopyKeyLogButton.Click += new EventHandler(CopyKeyButtonClicked);  
        AppUIShowKey.Controls.Add(CopyKeyLogButton);
    }

    private void CopyKeyButtonClicked(object? sender, EventArgs eventArgs) {
        string KeyForDecode = AppUIShowKey.ShowKeyForDecode.Text;
        if(string.IsNullOrWhiteSpace(KeyForDecode)) {
            MessageBox.Show("Key for decode is emty! No need copy to clipboard");
            return;
        }

        Clipboard.SetText(KeyForDecode);
        MessageBox.Show("Copied key successfuly!");
    }
}