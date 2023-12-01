using Avalonia.Controls;
using SchoolProjectA_ClientMVC.Controllers;
using SchoolProjectA_ClientMVC.Views.Log;
using SchoolProjectA_ClientMVC.Views.Menu;
namespace SchoolProjectA_ClientMVC.Views.Main;

public partial class MainWindow : Window
{
    public LogView LogControl {get;set;}
    public MenuView MenuControl {get;set;}
    private readonly ConnexionController CnxCtrl;
    public MainWindow()
    {
        LogControl = new LogView();
        MenuControl = new MenuView();
        CnxCtrl = new(this);

        InitializeComponent();

        FullPage.Content = LogControl;
    }
}