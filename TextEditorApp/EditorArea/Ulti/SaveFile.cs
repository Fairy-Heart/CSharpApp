using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace TextEditorApp.EditorArea.Ulti
{
    public partial class SaveFile
    {
        private readonly MainWindow MainWindow;
        public SaveFile(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }

        public void SaveFileAction(object? sender, RoutedEventArgs routedEventArgs)
        {
            try 
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();

                string FilePath = openFileDialog.FileName;
                string Text = MainWindow.EditorArea.Text;

                string SelectFileContent = File.ReadAllText(FilePath);

                if(string.IsNullOrEmpty(FilePath) || !File.Exists(FilePath))
                {
                    MessageBox.Show("No valid file selected");
                    return;
                }

                if(!string.IsNullOrEmpty(SelectFileContent))
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show(
                        "The file you choose already has content in it! If you click YES, all content in this session will overwrite the selected file",
                        "Confirm",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning
                    );

                    if(messageBoxResult == MessageBoxResult.No )
                    {
                        MessageBox.Show("Canceled");
                        return;
                    }


                    using (StreamWriter Writer = new StreamWriter(FilePath, false))
                    {
                        Writer.WriteLine(Text);
                    }

                    MessageBox.Show($"Successfully saved file as:\n{FilePath}");

                    return;
                }

                    using(StreamWriter Writer = new StreamWriter(FilePath, false))
                {
                    Writer.WriteLine(Text);

                    MessageBox.Show($"Successfully saved file as:\n{FilePath}");
                }
            }
            catch 
            {
                // Do nothing
            }   
        }
    }

}
