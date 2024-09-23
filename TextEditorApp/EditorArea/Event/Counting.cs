using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorApp.EditorArea.Event
{
    public partial class Counting
    {
        private readonly MainWindow MainWindow;

        public Counting(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }

        public void SetCounting(object? sender, EventArgs e)
        {
            int LineCount = MainWindow.EditorArea.LineCount;
            int TextLength = MainWindow.EditorArea.Text.Length;

            MainWindow.Counting.Content = "          " + $"Line: {LineCount}             " + $"Character: {TextLength}";
        }
    }
}
