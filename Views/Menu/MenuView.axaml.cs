using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SchoolProjectA_ClientMVC.Models;

namespace SchoolProjectA_ClientMVC.Views.Menu;

public partial class MenuView : UserControl
{
    public Moni? MyMoni {  get; set; }
    public MenuView()
    {
        InitializeComponent();

        //TextConnect.Text = $"Hello {User}"; // Ne marche pas, il faut plutôt le refresh
    }
}