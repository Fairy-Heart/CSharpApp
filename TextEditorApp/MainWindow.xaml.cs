using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly EditorArea.Event.AutoChangeSizeEditorArea ChangeSizeEditorAuto;
        private readonly EditorArea.Ulti.NewFile SetNewFile;
        private readonly EditorArea.Ulti.OpenFile SetOpenFile;
        private readonly EditorArea.Ulti.SaveFile SetSaveFile;
        private readonly EditorArea.Shorcut.Save SaveFileShortcut;
        private readonly EditorArea.Event.QuitSave QuitSave;
        private readonly EditorArea.Event.IsEdited IsEditedFile;
        private readonly EditorArea.Event.Counting SetCounting;
        public bool IsFileEdited = false;
        
        public MainWindow() {
            // Initialize component
            InitializeComponent();

            ChangeSizeEditorAuto = new EditorArea.Event.AutoChangeSizeEditorArea(this);
            this.SizeChanged += ChangeSizeEditorAuto.ChangeEditorSize;
            this.StateChanged += ChangeSizeEditorAuto.ChangeEditorSize;

            
            EditorArea.TextArea.TextView.LinkTextBackgroundBrush = null;
            EditorArea.TextArea.TextView.LinkTextForegroundBrush = Brushes.White;
            EditorArea.TextArea.TextView.LinkTextUnderline = true;


            // Set a new file function
            SetNewFile = new EditorArea.Ulti.NewFile(this);
            NewFile.Click += SetNewFile.NewFileSelectOption;

            // Set a open file dialog

            SetOpenFile = new EditorArea.Ulti.OpenFile(this);
            OpenFile.Click += SetOpenFile.FileDialog;

            // Set a save file dialog

            SetSaveFile = new EditorArea.Ulti.SaveFile(this);
            SaveFile.Click += SetSaveFile.SaveFileAction;


            // Shortcut

            SaveFileShortcut = new EditorArea.Shorcut.Save(this);
            this.KeyDown += SaveFileShortcut.SaveShortcut;

            // Ask user when exit but forget save file

            QuitSave = new EditorArea.Event.QuitSave(this);
            IsEditedFile = new EditorArea.Event.IsEdited(this);
            this.Closing += QuitSave.ConfirmExitApp;
            EditorArea.TextChanged += IsEditedFile.FileEditedAction;

            // Counting a line number, char, etc...

            SetCounting = new EditorArea.Event.Counting(this);
            EditorArea.TextChanged += SetCounting.SetCounting;

            // Default display
            int LineCount = EditorArea.LineCount;
            int TextLength = EditorArea.Text.Length;
            Counting.Content = "          " + $"Line: {LineCount}             " + $"Character: {TextLength}";
        }

    }
}