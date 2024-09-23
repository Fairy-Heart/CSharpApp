using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorApp.EditorArea.Event
{
    public partial class AutoChangeSizeEditorArea
    {
        private readonly MainWindow MainWindow;
        public AutoChangeSizeEditorArea(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }

        public void ChangeEditorSize(object? sender, EventArgs eventArgs) 
        {
            MainWindow.EditorArea.Width = MainWindow.ActualWidth - 20;
           
        }
    }
}
