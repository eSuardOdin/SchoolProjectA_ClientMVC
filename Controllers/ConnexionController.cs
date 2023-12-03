using Avalonia.Controls;
using Avalonia.Interactivity;
using SchoolProjectA_ClientMVC.Views.Main;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using SchoolProjectA_ClientMVC.Views.Log;
using SchoolProjectA_ClientMVC.Views.Menu;
using System;
using System.Text.RegularExpressions;
using SchoolProjectA_ClientMVC.Models;
using System.Runtime.CompilerServices;

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
        _view.LogControl.ConnectBtn.Click += HandleConnexionEvent;
        _view.LogControl.LoginTB.GotFocus += ResetControl;
        _view.LogControl.PwdTB.GotFocus += ResetControl;

        // Menu page
        _view.MenuControl.DisconnectBtn.Click += HandleDeconnexion;
    }

    private async Task HandleConnexion(object? sender, RoutedEventArgs e)
    {
        if(await ValidateConnexionInput())
        {
            System.Diagnostics.Debug.WriteLine($"{_view.MenuControl.MyMoni.FirstName} {_view.MenuControl.MyMoni.LastName}");
            //_view.MenuControl.User = _view.LogControl.LoginTB.Text;
            _view.FullPage.Content = _view.MenuControl;
        }
    }

    private void HandleConnexionEvent(object? sender, RoutedEventArgs e)
    {
        _ = HandleConnexion(sender, e); // Appel asynchrone de HandleConnexion
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
    private async Task<bool> ValidateConnexionInput()
    {
        bool isErrorFree = true;

        // Check emptyness
        if(_view.LogControl.LoginTB.Text?.Trim() == "" || _view.LogControl.LoginTB.Text is null)
        {
            _view.LogControl.LoginTB.Watermark = "Le login doit �tre renseign�";
            _view.LogControl.LoginTB.Background = Avalonia.Media.Brushes.Red;
            isErrorFree = false;
        }

        if (_view.LogControl.PwdTB.Text?.Trim() == "" || _view.LogControl.PwdTB.Text is null)
        {
            _view.LogControl.PwdTB.Watermark = "Le mot de passe doit �tre renseign�";
            _view.LogControl.PwdTB.Background = Avalonia.Media.Brushes.Red;
            isErrorFree = false;
        }

        // Gestion de la taille ? Ailleurs ?

        /* Check special login char
        Regex reg = new("^[a-zA-Z0-9]+$");
        if(isErrorFree && !reg.IsMatch(_view.LogControl.LoginTB.Text))
        {
            _view.LogControl.LoginTB.Watermark = "Doit �tre �tre compos� de symboles alphanum�riques";
            _view.LogControl.LoginTB.Background = Avalonia.Media.Brushes.Red;
            isErrorFree = false;
        }*/





        // Check query
        if (isErrorFree)
        {
            Moni moni = await Queries.GetMoni(_view.LogControl.LoginTB.Text);
            if (moni != null)
            {
                moni = await Queries.CheckMoni(moni, _view.LogControl.PwdTB.Text);
                if (moni != null) _view.MenuControl.MyMoni = moni;
                else isErrorFree = false;
            }
            else isErrorFree = false;
        }
        return isErrorFree;
    }


}

