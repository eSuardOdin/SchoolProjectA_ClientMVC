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
using System.Drawing;

namespace SchoolProjectA_ClientMVC.Controllers;

class GlobalController
{
    private readonly MainWindow _view;
    public GlobalController(MainWindow view)
    {
        _view = view;
        LinkEvents();
    }


    /// <summary>
    /// Link the buttons events from the 3 mains views
    /// </summary>
    private void LinkEvents()
    {
        // Log page
        _view.MyLogView.ConnectBtn.Click += HandleConnexionEvent;
        _view.MyLogView.LoginTB.GotFocus += ResetControl;
        _view.MyLogView.PwdTB.GotFocus += ResetControl;
        _view.MyLogView.CreateUserBtn.Click += ShowUserCreationView;

        // User creation
        _view.MyUserCreationView.CancelUserCreationBtn.Click += CancelUserCreation;
        _view.MyUserCreationView.CreateUserBtn.Click += CreateUser;

        // Menu page
        _view.MyMenuView.DisconnectBtn.Click += HandleDeconnexion;
    }

    
    
    //-----------------------
    //      O---------O
    //      | LogView |
    //      O---------O
    //-----------------------
    
    
    /// <summary>
    /// Handle the connexion to the application<br/>If ok, MenuView is shown withs its Moni attached
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
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

    
    
    
    /// <summary>
    /// Linked to the button click to lauch the asynchronous HandleConnexion
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HandleConnexionEvent(object? sender, RoutedEventArgs e)
    {
        _ = HandleConnexion(sender, e); // Appel asynchrone de HandleConnexion
    }




    /// <summary>
    /// Reset text boxes in LogView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ResetControl(object? sender, RoutedEventArgs e)
    {
        if (((TextBox)sender).Background == Avalonia.Media.Brushes.Red)
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
        if (_view.MyLogView.LoginTB.Text?.Trim() == "" || _view.MyLogView.LoginTB.Text is null)
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





    /// <summary>
    /// Checks Moni credentials
    /// </summary>
    /// <returns>Returns the moni if ok or null if not</returns>
    private async Task<Moni?> CheckMoniCredentials()
    {
        Moni? moni = await Queries.GetMoni(_view.MyLogView.LoginTB.Text!);
        if (moni != null)
        {
            moni = await Queries.CheckMoni(moni, _view.MyLogView.PwdTB.Text!);
        }
        return moni;
    }





    private void ShowUserCreationView(object? sender, RoutedEventArgs e)
    {
        _view.FullPage.Content = _view.MyUserCreationView;
        _view.MyLogView.QueryLbl.IsVisible = false;
        _view.MyLogView.LoginTB.Text = "";
        _view.MyLogView.PwdTB.Text = "";
    }




    //-----------------------
    //      O----------O
    //      | MenuView |
    //      O----------O
    //-----------------------


    /// <summary>
    /// Handles the deconnexion
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HandleDeconnexion(object? sender, RoutedEventArgs e)
    {
        _view.FullPage.Content = _view.MyLogView;
        _view.MyMenuView.MyMoni = null;
    }





    //-----------------------------
    //      O------------------O
    //      | UserCreationView |
    //      O------------------O
    //-----------------------------



    private void CancelUserCreation(object? sender, RoutedEventArgs e)
    {
        _view.FullPage.Content = _view.MyLogView;
        ResetUserCreationTextBoxes();
        ResetErrorLbl();
    }

    private bool CheckUserCreationInput()
    {
        var view = _view.MyUserCreationView;
        bool IsErrorFree = true;
        if(view.LastNameTB.Text?.Trim() == "" || view.LastNameTB.Text is null)
        {
            IsErrorFree = false;
            view.LastNameErrorLbl.Text = "Le champ de nom ne doit pas être vide.";
            view.LastNameErrorLbl.IsVisible = true;
        }
        if (view.FirstNameTB.Text?.Trim() == "" || view.FirstNameTB.Text is null)
        {
            IsErrorFree = false;
            view.FirstNameErrorLbl.Text = "Le champ de prénom ne doit pas être vide.";
            view.FirstNameErrorLbl.IsVisible = true;
        }
        if (view.LoginTB.Text?.Trim() == "" || view.LoginTB.Text is null)
        {
            IsErrorFree = false;
            view.LoginErrorLbl.Text = "Le champ de login ne doit pas être vide.";
            view.LoginErrorLbl.IsVisible = true;
        }
        if (view.LoginTB.Text?.Trim() == "" || view.LoginTB.Text is null)
        {
            IsErrorFree = false;
            view.LoginErrorLbl.Text = "Le champ de login ne doit pas être vide.";
            view.LoginErrorLbl.IsVisible = true;
        }

        if (view.PwdTB.Text?.Trim() == "" || view.PwdConfirmationTB.Text?.Trim() == "" || view.PwdConfirmationTB.Text is null || view.PwdTB.Text is null)
        {
            IsErrorFree = false;
            view.PwdErrorLbl.Text = "Les champ de mot de passe ne doivent pas être vide.";
            view.PwdErrorLbl.IsVisible = true;
        }
        if (view.PwdTB.Text?.Trim() != "" && view.PwdConfirmationTB.Text?.Trim() != "" && view.PwdConfirmationTB.Text != null && view.PwdTB.Text != null)
        {
            if (view.PwdTB.Text != view.PwdConfirmationTB.Text)
            {
                IsErrorFree = false;
                view.PwdErrorLbl.Text = "Les champ de mot de passe ne correspondent pas.";
                view.PwdErrorLbl.IsVisible = true;
            }
        }

        return IsErrorFree;
    }


    private async void CreateUser(object? sender, RoutedEventArgs e)
    {
        ResetErrorLbl();
        var view = _view.MyUserCreationView;
        view.CreateUserBtn.IsEnabled = false;
        view.CancelUserCreationBtn.IsEnabled = false;
        bool isErrorFree = CheckUserCreationInput();
        if(isErrorFree)
        {
            Moni? moni = await Queries.GetMoni(_view.MyUserCreationView.LoginTB.Text);
            if (moni != null)
            {
                view.LoginErrorLbl.IsVisible = true;
                view.LoginErrorLbl.Text = $"Le login {moni.MoniLogin} n'est pas disponible";
                isErrorFree = false;
            }
            if(isErrorFree)
            {

                Moni? targetMoni = null;
                moni = new() { MoniLogin = view.LoginTB.Text, MoniPwd = view.PwdTB.Text, FirstName = view.FirstNameTB.Text, LastName = view.LastNameTB.Text };
                targetMoni = await Queries.PostMoni(moni);

                view.QueryStatusLbl.IsVisible = true;
                if(targetMoni != null)
                {
                    view.QueryStatusLbl.Text = $"Le Moni {targetMoni.MoniLogin} a été créé avec succès.";
                    view.QueryStatusLbl.Foreground = Avalonia.Media.Brushes.Green;
                }
                else
                {
                    view.QueryStatusLbl.Text = $"Erreur lors de la création du Moni";
                    view.QueryStatusLbl.Foreground = Avalonia.Media.Brushes.Red;
                }
            }
        }
        view.CancelUserCreationBtn.IsEnabled = true;
        view.CreateUserBtn.IsEnabled = true;
    }


    private void ResetErrorLbl()
    {
        var view = _view.MyUserCreationView;
        view.LastNameErrorLbl.IsVisible = false;
        view.FirstNameErrorLbl.IsVisible = false;
        view.LoginErrorLbl.IsVisible = false;
        view.PwdErrorLbl.IsVisible = false;
        view.QueryStatusLbl.IsVisible = false;
    }

    private void ResetUserCreationTextBoxes()
    {
        var view = _view.MyUserCreationView;
        view.LoginTB.Text = "";
        view.PwdTB.Text = "";
        view.PwdConfirmationTB.Text = "";
        view.FirstNameTB.Text = "";
        view.LastNameTB.Text = "";
    }



}

