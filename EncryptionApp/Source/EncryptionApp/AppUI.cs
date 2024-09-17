using EncryptionApp.EncryptionText;

namespace EncryptionApp;

public  partial class AppUI : Form {
    public readonly RichTextBox EnterTextBox;
    public readonly Label InputTextDescription;
    private readonly Button SendTextButton;

    private readonly EncryptionText.InputText InputTextEncryption;
    private readonly EncryptionText.ResultLog ResultLog;
    private readonly Label  LogDescription;
    public readonly RichTextBox LogResult;

    public readonly RichTextBox ShowKeyForDecode;
    private readonly Label ShowKeyDescription;
    private readonly ShowKey ShowKeyDecode;
    
    private readonly Button CopyKeyButton;
    private readonly Button CopyLogResultButton;

    public readonly RichTextBox KeyDecode;
    private readonly Label KeyDecodeDescription;
    private readonly DecodeText DecodeTextEncryption;
    
    private readonly Button DecodeButton;
    public AppUI(){
        Text = "Encryption App";
        Size = new Size(1200, 600);
        StartPosition = FormStartPosition.CenterScreen;
        Visible = true;
        BackColor = Color.FromArgb(33, 33, 33);
        

        // Method to encryption text.

        EnterTextBox = new RichTextBox();
        InputTextDescription = new  Label();
        InputTextEncryption = new EncryptionText.InputText(this);
        InputTextEncryption.SetInputText(EnterTextBox, InputTextDescription);

        // End.

        SendTextButton = new Button();
        InputTextEncryption.SendDataForEncryptionTextButton(SendTextButton);

        // Method to show result:

        ResultLog = new ResultLog(this);
        LogDescription = new Label();
        LogResult = new RichTextBox();
        CopyLogResultButton = new Button();
        ResultLog.ShowResultLog(LogResult, LogDescription);
        ResultLog.CopyResultLog(CopyLogResultButton);

        // End.

        // Method to show key and copy this:

        ShowKeyDecode = new ShowKey(this);
        ShowKeyForDecode = new RichTextBox();
        ShowKeyDescription = new Label();
        CopyKeyButton = new Button();
        ShowKeyDecode.ShowKeyLog(ShowKeyForDecode, ShowKeyDescription);
        ShowKeyDecode.CopyKeyLog(CopyKeyButton);
        
        // End.

        // Method to input key to decode text encryption

        DecodeTextEncryption = new DecodeText(this);
        KeyDecode = new RichTextBox();
        KeyDecodeDescription = new Label();
        DecodeButton = new Button();
        DecodeTextEncryption.InputKeyToDecodeText(KeyDecode, KeyDecodeDescription);
        DecodeTextEncryption.InputKeyToDecodeTextButton(DecodeButton);        
    }

}
