using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TextEditorApp.EditorArea.Ulti
{
    public partial class NewFile
    {
        private readonly MainWindow MainWindow;
        public NewFile(MainWindow MainWindow) 
        {
            this.MainWindow = MainWindow;
        }

        // When item menu "New File" clicked
        public void NewFileSelectOption(object? sender, RoutedEventArgs eventArgs) 
        {
            MainWindow.Title = "New File - Text Editor";
        }

    }
}
