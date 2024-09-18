using System;
using System.Windows.Forms;

namespace EncryptionApp.EncryptionText.Options.Menu;

public partial class Menu {

    private readonly AppUI AppUIMenu;
    public Menu(AppUI AppUIMenu) {
        this.AppUIMenu = AppUIMenu;
    }

    public void SetMenuForEncryptMethod(ComboBox MethodSelect, Label Description) {
        Description.Text = "Select Encryption Method";
        Description.Location = new Point(330, 360);
        Description.ForeColor = Color.White;
        Description.Width = 250;
        Description.Height = 20;


        MethodSelect.Location = new Point(330, 325);
        MethodSelect.Width = 50;

        MethodSelect.Items.Add("AES Encryption");
        MethodSelect.Items.Add("Base64 Encryption");
        MethodSelect.Items.Add("Hash with SHA512");
        MethodSelect.Width = 200;
        MethodSelect.Height = 30;
        MethodSelect.DropDownStyle = ComboBoxStyle.DropDownList;
        MethodSelect.SelectedIndex = 0;

        AppUIMenu.Controls.Add(MethodSelect);
        AppUIMenu.Controls.Add(Description);
        
    }
}