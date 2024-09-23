using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TextEditorApp.EditorArea.Shorcut
{
    public partial class Save
    {
        private readonly MainWindow MainWindow;
        public Save(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }

        public void SaveShortcut(object? sender, KeyEventArgs keyEventArgs) 
        {
            if(Keyboard.Modifiers == ModifierKeys.Control && keyEventArgs.Key == Key.S)
            {
                MessageBox.Show("Shortcut started");
                keyEventArgs.Handled = true;
            }
        }
    }
}
