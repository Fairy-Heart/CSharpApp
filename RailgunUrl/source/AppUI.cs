using System;
using System.Drawing;
using System.Windows.Forms;
namespace RailgunUrl;
public partial class AppUI : Form {

    public TextBox urlInput;
    public ComboBox selectMethodBox;

    public RichTextBox dataInput;

    public RichTextBox resultLog;

    public Button submitButton;

    public Request request;

    public Label waitingNotify;

    public Button clearLog;

    public ClearLog clearLogMethod;

    public Button saveLog;

    public SaveLog saveLogAsFile;

    public CopyLog copyLog;

    public Button copyLogButton;
    public AppUI() {
        // HTTP, GET, POST, DELETE
        request = new Request(this);

        // Clear log
        clearLogMethod = new ClearLog(this);

        // Save log as file
        saveLogAsFile = new SaveLog(this);

        // Copy log to clipboard
        copyLog = new CopyLog(this);

        // Default app property and theme
        this.Text = "Railgun URL Tool";
        this.Size = new Size(1200, 600);
        this.BackColor = Color.FromArgb(33, 33, 33); // HEX COLOR : #212121
        // Always center
        this.StartPosition = FormStartPosition.CenterScreen;
        this.MaximizeBox= false;
        this.Icon = new Icon("./icon.ico");


        // Call method
        urlInput = new TextBox();
        InputURL();

        selectMethodBox = new ComboBox();
        SelectMethodBox();

        dataInput = new RichTextBox();
        InputData();

        submitButton = new Button();
        SubmitButton();

        waitingNotify = new Label();

        resultLog = new RichTextBox();
        ShowResultLog();

        clearLog = new Button();
        ClearLog();

        saveLog = new Button();
        SaveLog();

        copyLogButton = new Button();
        CopyLog();
    }


    private void InputURL() {
        Label urlInputLabel;
        urlInputLabel = new Label();

        urlInput.Location = new System.Drawing.Point(70, 600);
        urlInput.Top -= 540;
        urlInput.Width = 300;
        urlInput.Height = 200;
        this.Controls.Add(urlInput);

        urlInputLabel.Location = new System.Drawing.Point(70, 600);
        urlInputLabel.Top -= 565;
        urlInputLabel.Text = "Input URL:";
        urlInputLabel.ForeColor = Color.White;
        this.Controls.Add(urlInputLabel);
    }

    private void SelectMethodBox() {
        Label selectMethodBoxLabel = new Label();

        selectMethodBox.Location = new System.Drawing.Point(400, 700);
        selectMethodBox.Top -= 640;
        selectMethodBox.Width = 70;

        selectMethodBox.Items.Add("GET");
        selectMethodBox.Items.Add("POST");
        selectMethodBox.Items.Add("DELETE");
        selectMethodBox.SelectedIndex = 0;
        

        selectMethodBox.DropDownStyle = ComboBoxStyle.DropDownList;

        selectMethodBoxLabel.Text = "Select Method:";
        selectMethodBoxLabel.ForeColor = Color.White;
        selectMethodBoxLabel.Location = new System.Drawing.Point(390, 700);
        selectMethodBoxLabel.Top -= 665;

        this.Controls.Add(selectMethodBox);
        this.Controls.Add(selectMethodBoxLabel);
    }


    private void InputData() {
        Label dataInputLabel = new Label();

        dataInput.Location = new System.Drawing.Point(70, 700);
        dataInput.Multiline = true;
        dataInput.Width = 400;
        dataInput.Height = 300;

        dataInput.Top -= 570;

        dataInputLabel.ForeColor = Color.White;
        dataInputLabel.Location = new System.Drawing.Point(70, 700);
        dataInputLabel.Top -= 595;
        dataInputLabel.Width = 200;
        dataInputLabel.Text = "Input Data (Optional):";

        this.Controls.Add(dataInput);
        this.Controls.Add(dataInputLabel);
    }


    private void SubmitButton() {
        submitButton.Text = "Send";
        submitButton.Location = new System.Drawing.Point(70, 700);
        submitButton.Top -= 250;
        submitButton.Height = 50;   
        submitButton.BackColor = Color.White;
        submitButton.FlatStyle = FlatStyle.Flat;

        submitButton.Click += new EventHandler(request.SubmitButtonClicked);
        this.Controls.Add(submitButton);
    }


    private void ShowResultLog() {
        waitingNotify.Text = "Result:";
        waitingNotify.Location = new System.Drawing.Point(500, 900);
        waitingNotify.Top -= 795;
        waitingNotify.ForeColor = Color.White;

        resultLog.Location = new System.Drawing.Point(500, 900);
        resultLog.Top -= 770;

        resultLog.Width = 640;

        resultLog.Height = 300;
        resultLog.ReadOnly = true;

        this.Controls.Add(resultLog);
        this.Controls.Add(waitingNotify);
    }
    
    private void ClearLog() {
        clearLog.Text = "Clear Log";
        clearLog.Height = 50;
        clearLog.FlatStyle = FlatStyle.Flat;
        clearLog.Location = new System.Drawing.Point(500, 900);
        clearLog.Top -=  449;
        clearLog.BackColor = Color.White;

        clearLog.Click += new EventHandler(clearLogMethod.ClearLogButtonClicked);
        this.Controls.Add(clearLog);
    }

    private void SaveLog() {
        saveLog.Text = "Save Log";
        saveLog.Height = 50;
        saveLog.FlatStyle = FlatStyle.Flat;
        saveLog.Location = new System.Drawing.Point(600, 900);
        saveLog.Top -= 449;
        saveLog.BackColor = Color.White;

        saveLog.Click += new EventHandler(saveLogAsFile.SaveLogAs);
        this.Controls.Add(saveLog);
    }

    private void CopyLog() {
        copyLogButton.Text = "Copy Log";
        copyLogButton.Height = 50;
        copyLogButton.FlatStyle = FlatStyle.Flat;
        copyLogButton.Location = new System.Drawing.Point(700, 900);
        copyLogButton.Top -= 449;
        copyLogButton.BackColor = Color.White;

        copyLogButton.Click += new EventHandler(copyLog.CopyLogButtonClicked);

        this.Controls.Add(copyLogButton);
    }

}
