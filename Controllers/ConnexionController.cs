using Avalonia.Controls;
using Avalonia.Interactivity;
using SchoolProjectA_ClientMVC.Views.Main;
using SchoolProjectA_ClientMVC.Views.Log;
using SchoolProjectA_ClientMVC.Views.Menu;
using System;

namespace SchoolProjectA_ClientMVC.Controllers;

class ConnexionController
{
    private readonly MainWindow _view;
    public ConnexionController(MainWindow view)
    {
        _view = view;
        ConfigureConnexionButtons();
    }

    private void ConfigureConnexionButtons()
    {
        _view.LogControl.ConnectBtn.Click += HandleConnexion;
        _view.MenuControl.DisconnectBtn.Click += HandleDeconnexion;
    }

    private void HandleConnexion(object? sender, RoutedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("ok" + _view.LogControl.LoginTB.Text);
        _view.MenuControl.User = _view.LogControl.LoginTB.Text;
        _view.FullPage.Content = _view.MenuControl;
    }

    private void HandleDeconnexion(object? sender, RoutedEventArgs e)
    {
        _view.FullPage.Content = _view.LogControl;
    }
}

