namespace RailgunUrl;

public partial class SaveLog {
    private AppUI appUI;
    public SaveLog(AppUI appUI) {
        this.appUI = appUI;
    }

    public void SaveLogAs(object? sender, EventArgs e) {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        openFileDialog.Title = "Save log as:";
        openFileDialog.Filter = "TXT File (*.txt)|*.txt";

       

        if(openFileDialog.ShowDialog() == DialogResult.OK) {
            string? path = openFileDialog.FileName;
            string? logResult = appUI.resultLog.Text;
            if(string.IsNullOrWhiteSpace(logResult)) {
                MessageBox.Show("Log result is emty, no need save as a file");
                return;
            }
            using(StreamWriter streamWriter = new StreamWriter(path, false)) {
                streamWriter.Write(logResult);
            }
            MessageBox.Show($"Save done as: {path}");
        }
    }
}