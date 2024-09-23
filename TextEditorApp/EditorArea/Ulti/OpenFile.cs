using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace TextEditorApp.EditorArea.Ulti
{
    public partial class OpenFile
    {
        private readonly MainWindow MainWindow;
        public OpenFile(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }

        public void FileDialog(object? sender, RoutedEventArgs routedEventArgs) 
        { 
            OpenFileDialog openFileDialog = new OpenFileDialog();

            try
            {
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.ShowDialog();

                List<string> BinaryFiles = new List<string>
                {
                    ".exe",
                    ".dll",
                    ".sys",
                    ".dat",
                    ".com",
                    ".ocx",
                    ".msi",
                    ".img",
                    ".iso"
                };
  
                
                string FilePath = openFileDialog.FileName;

                string FileContent = File.ReadAllText(FilePath);
                
                string FileName = Path.GetFileName(FilePath);

                string FileExtension = Path.GetExtension(FilePath);

                if(string.IsNullOrEmpty(FilePath) || !File.Exists(FilePath))
                {
                    MessageBox.Show("No valid file selected");
                    return;
                }

                if (BinaryFiles.Contains(FileExtension.ToLower())) {
                    MessageBoxResult messageBoxResult = MessageBox.Show(
                            "The file you seleted could be binary file. Opening can cause lag/crash " +
                            "app. Open anyway?",
                            "Confirm",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning
                        );

                    if(messageBoxResult == MessageBoxResult.Yes)
                    {
                        MainWindow.Title = $"{FileName} - Text Editor";
                        MainWindow.EditorArea.Text = FileContent;
                        MainWindow.IsFileEdited = false;
                        return;
                    }

                    if(messageBoxResult == MessageBoxResult.No)
                    {
                        MessageBox.Show("Canceled action");
                    }
                    
                }

                // If file is not binary types
                MainWindow.Title = $"{FileName} - Text Editor";
                MainWindow.EditorArea.Text = FileContent;
                MainWindow.IsFileEdited = false;

            } 
            catch
            {
               // Do nothing
            }
        }
    }
}
