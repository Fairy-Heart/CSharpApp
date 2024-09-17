using System;
using System.Windows.Forms;

namespace EncryptionApp.EncryptionText;


public partial class ResultLog {
    private readonly AppUI AppUIResultLog;
    public readonly RichTextBox LogResult;
    public ResultLog(AppUI AppUIResultLog) {
        this.AppUIResultLog = AppUIResultLog;
        LogResult = new RichTextBox();
    }

    public void ShowResultLog(RichTextBox LogResult, Label Description) {
        Description.Text = "Decode/Encryption text result:";
        Description.Location = new Point(600, 35);
        Description.ForeColor = Color.White;
        Description.Width = 200;

        LogResult.Location = new Point (600, 55);
        LogResult.Size = new Size(500, 250);
        LogResult.ReadOnly = true;
        AppUIResultLog.Controls.Add(LogResult);
        AppUIResultLog.Controls.Add(Description);
    }


    public void CopyResultLog(Button CopyResultLogButton) {
        CopyResultLogButton.Text = "Copy Text/Encryption Text";
        CopyResultLogButton.BackColor = Color.White;
        CopyResultLogButton.Width = 200;
        CopyResultLogButton.Height = 30;
        CopyResultLogButton.Location = new Point(600, 320);
        CopyResultLogButton.Click += new EventHandler(CopyResultButtonClicked);

        AppUIResultLog.Controls.Add(CopyResultLogButton);
    }

    private void CopyResultButtonClicked(object? sender, EventArgs eventArgs) {
        string LogResultText = AppUIResultLog.LogResult.Text;

        if(string.IsNullOrWhiteSpace(LogResultText)) {
            MessageBox.Show("Log result is emty!No need copy to clipboard");
            return;
        }
        MessageBox.Show("Successfuly copied to your clipboard");
        Clipboard.SetText(LogResultText);
    }


}