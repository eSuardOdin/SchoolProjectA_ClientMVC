using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SchoolProjectA_ClientMVC.Views.Menu;

public partial class MenuView : UserControl
{
    public string User { get; set; } // Simule le moni
    public MenuView()
    {
        InitializeComponent();

        TextConnect.Text = $"Hello {User}"; // Ne marche pas, il faut plutôt le refresh
    }
}