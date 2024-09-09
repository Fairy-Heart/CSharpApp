using System;
using System.Windows.Forms;
namespace RailgunUrl;
public partial class CopyLog {
    private AppUI appUI;
    public CopyLog(AppUI appUI) {
        this.appUI = appUI;
    }

    public void CopyLogButtonClicked(object? sender, EventArgs e) {
        string? logResult = appUI.resultLog.Text;
        if(string.IsNullOrWhiteSpace(logResult)) {
            MessageBox.Show("Log Result is emty. No need copy to clipboard");
            return;
        }

        Clipboard.SetText(logResult);
        appUI.copyLogButton.Text = "Copied";
    }
}