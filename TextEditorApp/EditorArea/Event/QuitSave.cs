using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TextEditorApp.EditorArea.Event
{
    public partial class QuitSave
    {
        private readonly MainWindow MainWindow;

        public QuitSave(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }

        public void ConfirmExitApp(object? sender, System.ComponentModel.CancelEventArgs cancelEventArgs)
        {
            if(MainWindow.IsFileEdited == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(
                "Confirm! Your file is not saved, quit anyway?",
                "File is not saved",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
                    );
                if(messageBoxResult == MessageBoxResult.Yes)
                {
                    return;
                }

                if(messageBoxResult == MessageBoxResult.No)
                {   
                    try
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.ShowDialog();

                        openFileDialog.Title = "Save file as..";
                        openFileDialog.Filter = "All files (*.*)|*.*";
                        string FilePath = openFileDialog.FileName;

                        string TextSaved = MainWindow.EditorArea.Text;

                        if(string.IsNullOrEmpty(FilePath) || !File.Exists(FilePath))
                        {
                            MessageBox.Show("Canceled save file");
                            return;
                        }

                        using (StreamWriter Writer = new StreamWriter(FilePath, false))
                        {
                            Writer.WriteLine(TextSaved);
                            MainWindow.Close();
                        }

                    } catch
                    {
                        // Do nothing
                    }
                    
                    
                }
            }

            MainWindow.Close();
        }
    }
}
