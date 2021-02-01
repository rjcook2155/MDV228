using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AllTheRickAndMorty
{
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
            
            if (string.IsNullOrEmpty(firstNameEntry.Text) == true || string.IsNullOrEmpty(lastNameEntry.Text) == true || string.IsNullOrEmpty(userNameEntry.Text) == true || string.IsNullOrEmpty(emailEntry.Text) == true || string.IsNullOrEmpty(passwordEntry.Text) == true)
            {
                DisplayAlert("Oops!", "Please make sure nothing is left blank to continue!", "Okay");
                firstNameEntry.Text = "";
                lastNameEntry.Text = "";
                userNameEntry.Text = "";
                emailEntry.Text = "";
                passwordEntry.Text = "";
                passwordRetype.Text = "";
            }
            else if (passwordEntry.Text != passwordRetype.Text)
            {
                DisplayAlert("Oops!", "Please make sure your passwords match!", "Okay");
                firstNameEntry.Text = "";
                lastNameEntry.Text = "";
                userNameEntry.Text = "";
                emailEntry.Text = "";
                passwordEntry.Text = "";
                passwordRetype.Text = "";
            }
            else if (UserData.DoesUsernameExist(userNameEntry.Text) == true)
            {
                DisplayAlert("Sorry!", "That username already exists! Please try another one.", "Okay");
                firstNameEntry.Text = "";
                lastNameEntry.Text = "";
                userNameEntry.Text = "";
                emailEntry.Text = "";
                passwordEntry.Text = "";
                passwordRetype.Text = "";
            }
            else if (UserData.DoesEmailExist(emailEntry.Text) == true)
            {
                DisplayAlert("Sorry!", "There is already an account with that email! Please choose another one.", "Okay");
                firstNameEntry.Text = "";
                lastNameEntry.Text = "";
                userNameEntry.Text = "";
                emailEntry.Text = "";
                passwordEntry.Text = "";
                passwordRetype.Text = "";
            }
            else
            {
                UserData newUser = new UserData();
                newUser.FirstName = firstNameEntry.Text;
                newUser.LastName = lastNameEntry.Text;
                newUser.UserName = userNameEntry.Text;
                newUser.Email = emailEntry.Text;
                newUser.Password = passwordEntry.Text;
                UserData.SaveUsers(newUser);
                DisplayAlert("New User Created!", "", "Okay");
                Navigation.PushAsync(new TabbedPageMain());
            }

        }
    }
}