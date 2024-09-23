using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TextEditorApp.EditorArea.Event
{
    public partial class IsEdited
    {
        private readonly MainWindow MainWindow;

        public IsEdited(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }

        public void FileEditedAction(object? sender, EventArgs eventArgs)
        {
            MainWindow.IsFileEdited = true;
        }
    }
}
