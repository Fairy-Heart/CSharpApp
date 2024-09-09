using System;
using System.Windows.Forms;
namespace RailgunUrl;

public partial class ClearLog {

    private AppUI appUI;
    public ClearLog(AppUI appUI) {
        this.appUI = appUI;
    }
    public void ClearLogButtonClicked(object? sender, EventArgs e) {
        appUI.resultLog.Text = "";
    }
}