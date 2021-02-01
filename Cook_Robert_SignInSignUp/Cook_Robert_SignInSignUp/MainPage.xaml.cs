using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Cook_Robert_SignInSignUp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Cook_Robert_SignInSignUp
{
    /*
     * Robert Cook
     * MDV228
     * 01/17/2021
     */
    public partial class MainPage : ContentPage
    {
        List<UserData> AllUsers = new List<UserData>();

        public MainPage()
        {
            InitializeComponent();
            // Pull users from List in UserData class
            AllUsers = UserData.GetUsers();
            // Assign functions to buttons on xaml 
            createButton.Clicked += CreateButton_Clicked;
            signInButton.Clicked += SignInButton_Clicked;
        }
        // Move to Sign In page once SignIn button is clicked
        private void SignInButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInPage());
        }
        // Creates users account and saves the information
        private void CreateButton_Clicked(object sender, EventArgs e)
        {
            UserData newUser = new UserData();
            newUser.FirstName = firstNameEntry.Text;
            newUser.LastName = lastNameEntry.Text;
            newUser.UserName = userNameEntry.Text;
            newUser.Email = emailEntry.Text;
            newUser.Password = passwordEntry.Text;

            if (newUser.Password != passwordRetype.Text)
            {
                DisplayAlert("Oops!", "Please make sure your passwords match!", "Okay");
                firstNameEntry.Text = "";
                lastNameEntry.Text = "";
                userNameEntry.Text = "";
                emailEntry.Text = "";
                passwordEntry.Text = "";
                passwordRetype.Text = "";
            }
            else if (UserData.DoesUsernameExist(newUser.UserName) == true)
            {
                DisplayAlert("Sorry!", "That username already exists! Please try another one.", "Okay");
                firstNameEntry.Text = "";
                lastNameEntry.Text = "";
                userNameEntry.Text = "";
                emailEntry.Text = "";
                passwordEntry.Text = "";
                passwordRetype.Text = "";
            }
            else if (UserData.DoesEmailExist(newUser.Email) == true)
            {
                DisplayAlert("Sorry!", "There is already an account with that email! Please choose another one.", "Okay");
                firstNameEntry.Text = "";
                lastNameEntry.Text = "";
                userNameEntry.Text = "";
                emailEntry.Text = "";
                passwordEntry.Text = "";
                passwordRetype.Text = "";
            }
            else if (newUser.FirstName == null || newUser.LastName == null || newUser.UserName == null || newUser.Email == null || newUser.Password == null)
            {
                DisplayAlert("Oops!", "Please make sure nothing is left blank to continue!", "Okay");
                firstNameEntry.Text = "";
                lastNameEntry.Text = "";
                userNameEntry.Text = "";
                emailEntry.Text = "";
                passwordEntry.Text = "";
                passwordRetype.Text = "";
            }
            else
            {
                UserData.SaveUsers(newUser);                
                DisplayAlert("New User Created!", "", "Okay");
                
            }
            
        }
    }
}
