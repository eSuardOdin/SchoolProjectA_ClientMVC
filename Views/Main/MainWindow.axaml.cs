using Avalonia.Controls;
using SchoolProjectA_ClientMVC.Controllers;
using SchoolProjectA_ClientMVC.Views.Log;
using SchoolProjectA_ClientMVC.Views.Menu;
namespace SchoolProjectA_ClientMVC.Views.Main;

public partial class MainWindow : Window
{
    public LogView MyLogView {get;set;}
    public MenuView MyMenuView {get;set;}
    public UserCreationView MyUserCreationView { get;set;}
    private readonly GlobalController MyGlobalController;
    public MainWindow()
    {
        MyLogView = new LogView();
        MyMenuView = new MenuView();
        MyUserCreationView = new UserCreationView();
        MyGlobalController = new(this);

        InitializeComponent();

        FullPage.Content = MyLogView;
    }
}