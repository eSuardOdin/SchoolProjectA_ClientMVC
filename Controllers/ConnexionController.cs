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

class GlobalController
{
    private readonly MainWindow _view;
    public GlobalController(MainWindow view)
    {
        _view = view;
        LinkEvents();
    }

    private void LinkEvents()
    {
        // Log page
        _view.MyLogView.ConnectBtn.Click += HandleConnexionEvent;
        _view.MyLogView.LoginTB.GotFocus += ResetControl;
        _view.MyLogView.PwdTB.GotFocus += ResetControl;

        // Menu page
        _view.MyMenuView.DisconnectBtn.Click += HandleDeconnexion;
    }

    private async Task HandleConnexion(object? sender, RoutedEventArgs e)
    {
        if(ValidateConnexionInput())
        {
            //System.Diagnostics.Debug.WriteLine($"{_view.MenuControl.MyMoni.FirstName} {_view.MenuControl.MyMoni.LastName}");
            //_view.MenuControl.User = _view.LogControl.LoginTB.Text;
            _view.MyLogView.ConnectBtn.IsEnabled = false;
            Moni? moni = await CheckMoniCredentials();
            if(moni == null)
            {
                _view.MyLogView.QueryLbl.IsVisible = true;
                _view.MyLogView.QueryLbl.Text = "Le mot de passe et login ne correspondent pas.";
            }
            else
            {
                _view.MyLogView.QueryLbl.IsVisible = false;
                _view.MyLogView.LoginTB.Text = "";
                _view.MyLogView.PwdTB.Text = "";
                _view.MyMenuView.MyMoni = moni;
                System.Diagnostics.Debug.WriteLine($"{_view.MyMenuView.MyMoni.FirstName} {_view.MyMenuView.MyMoni.LastName}");
                _view.FullPage.Content = _view.MyMenuView;
            }
            _view.MyLogView.ConnectBtn.IsEnabled = true;
        }
    }

    private void HandleConnexionEvent(object? sender, RoutedEventArgs e)
    {
        _ = HandleConnexion(sender, e); // Appel asynchrone de HandleConnexion
    }


    private void HandleDeconnexion(object? sender, RoutedEventArgs e)
    {
        _view.FullPage.Content = _view.MyLogView;
        _view.MyMenuView.MyMoni = null;
    }

    private void ResetControl(object? sender, RoutedEventArgs e)
    {
        if(((TextBox)sender).Background == Avalonia.Media.Brushes.Red)
        {
            ((TextBox)sender).Background = Avalonia.Media.Brushes.White;
            ((TextBox)sender).Watermark = ((TextBox)sender).Name == "LoginTB" ? "Login" : "Password";
        }
    }


    // à passer en sync + move la query dans une async
    /// <summary>
    /// Checks errors in input when trying to connect to application + change colors of input.
    /// </summary>
    /// <returns></returns>
    private bool ValidateConnexionInput()
    {
        bool isErrorFree = true;

        // Check emptyness
        if(_view.MyLogView.LoginTB.Text?.Trim() == "" || _view.MyLogView.LoginTB.Text is null)
        {
            _view.MyLogView.LoginTB.Watermark = "Le login doit être renseigné";
            _view.MyLogView.LoginTB.Background = Avalonia.Media.Brushes.Red;
            isErrorFree = false;
        }

        if (_view.MyLogView.PwdTB.Text?.Trim() == "" || _view.MyLogView.PwdTB.Text is null)
        {
            _view.MyLogView.PwdTB.Watermark = "Le mot de passe doit être renseigné";
            _view.MyLogView.PwdTB.Background = Avalonia.Media.Brushes.Red;
            isErrorFree = false;
        }
        return isErrorFree;
    }

    private async Task<Moni?> CheckMoniCredentials()
    {
        Moni? moni = await Queries.GetMoni(_view.MyLogView.LoginTB.Text!);
        if (moni != null)
        {
            moni = await Queries.CheckMoni(moni, _view.MyLogView.PwdTB.Text!);
        }
        return moni;
    }


}

