using Avalonia.Controls;
using Avalonia.Interactivity;
using SchoolProjectA_ClientMVC.Views.Main;
using Avalonia.Markup.Xaml;
using SchoolProjectA_ClientMVC.Views.Log;
using SchoolProjectA_ClientMVC.Views.Menu;
using System;
using System.Text.RegularExpressions;

namespace SchoolProjectA_ClientMVC.Controllers;

class ConnexionController
{
    private readonly MainWindow _view;
    public ConnexionController(MainWindow view)
    {
        _view = view;
        LinkEvents();
    }

    private void LinkEvents()
    {
        // Log page
        _view.LogControl.ConnectBtn.Click += HandleConnexion;
        _view.LogControl.LoginTB.GotFocus += ResetControl;
        _view.LogControl.PwdTB.GotFocus += ResetControl;

        // Menu page
        _view.MenuControl.DisconnectBtn.Click += HandleDeconnexion;
    }

    private void HandleConnexion(object? sender, RoutedEventArgs e)
    {
        if(ValidateConnexionInput())
        {
            System.Diagnostics.Debug.WriteLine("ok" + _view.LogControl.LoginTB.Text);
            _view.MenuControl.User = _view.LogControl.LoginTB.Text;
            _view.FullPage.Content = _view.MenuControl;
        }
    }

    private void HandleDeconnexion(object? sender, RoutedEventArgs e)
    {
        _view.FullPage.Content = _view.LogControl;
    }

    private void ResetControl(object? sender, RoutedEventArgs e)
    {
        if(((TextBox)sender).Background == Avalonia.Media.Brushes.Red)
        {
            ((TextBox)sender).Background = Avalonia.Media.Brushes.White;
            ((TextBox)sender).Watermark = ((TextBox)sender).Name == "LoginTB" ? "Login" : "Password";
        }
    }

    /// <summary>
    /// Checks errors in input when trying to connect to application + change colors of input.
    /// </summary>
    /// <returns></returns>
    private bool ValidateConnexionInput()
    {
        bool isErrorFree = true;

        // Check emptyness
        if(_view.LogControl.LoginTB.Text?.Trim() == "" || _view.LogControl.LoginTB.Text is null)
        {
            _view.LogControl.LoginTB.Watermark = "Le login doit être renseigné";
            _view.LogControl.LoginTB.Background = Avalonia.Media.Brushes.Red;
            isErrorFree = false;
        }

        if (_view.LogControl.PwdTB.Text?.Trim() == "" || _view.LogControl.PwdTB.Text is null)
        {
            _view.LogControl.PwdTB.Watermark = "Le mot de passe doit être renseigné";
            _view.LogControl.PwdTB.Background = Avalonia.Media.Brushes.Red;
            isErrorFree = false;
        }

        // Gestion de la taille ? Ailleurs ?

        // Check special login char
        Regex reg = new("^[a-zA-Z0-9]+$");
        if(isErrorFree && !reg.IsMatch(_view.LogControl.LoginTB.Text))
        {
            _view.LogControl.LoginTB.Watermark = "Doit être être composé de symboles alphanumériques";
            _view.LogControl.LoginTB.Background = Avalonia.Media.Brushes.Red;
            isErrorFree = false;
        }

        



        // Check query

        return isErrorFree;
    }


}

