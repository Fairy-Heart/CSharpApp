using EncryptionApp.EncryptionText;
using EncryptionApp.EncryptionText.Options.Menu;

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
    public readonly Label KeyDecodeDescription;
    private readonly DecodeText DecodeTextEncryption;
    
    private readonly Button DecodeButton;

    private readonly EncryptionText.Options.FileEncryptionMethod.FileEncryption EncryptionFileMethod;
    private readonly Button FileEncryptionButton;

    private readonly EncryptionText.Options.FileEncryptionMethod.DecodeFile DecodeFile;
    private readonly Button DecodeFileButton;

    private readonly EncryptionText.Options.Menu.Menu SelectMenu;
    public readonly ComboBox MenuSelectMethod;
    private readonly Label MenuDescription;

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

        // End.

        // Method to encryption file:

        EncryptionFileMethod = new EncryptionText.Options.FileEncryptionMethod.FileEncryption(this);
        FileEncryptionButton = new Button();
        EncryptionFileMethod.OpenFileToEncryptionButton(FileEncryptionButton);

        // End.

        // Method to decode file encrypted:

        DecodeFile = new EncryptionText.Options.FileEncryptionMethod.DecodeFile(this);
        DecodeFileButton = new Button();
        DecodeFile.DecodeFileButton(DecodeFileButton);

        // End.

        // Method to select - encryption methods:
        SelectMenu = new EncryptionText.Options.Menu.Menu(this);
        MenuSelectMethod = new ComboBox();
        MenuDescription = new Label();
        SelectMenu.SetMenuForEncryptMethod(MenuSelectMethod, MenuDescription);

        // End.
    }

}
