using System;
using System.Windows.Forms;
namespace RailgunUrl;

public static class App{

    [STAThread]
    static void Main() {
        ApplicationConfiguration.Initialize();
        Application.Run(new AppUI());
    }   
    
}