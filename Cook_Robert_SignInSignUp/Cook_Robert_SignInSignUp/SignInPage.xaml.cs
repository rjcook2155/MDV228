using System;
using System.Collections.Generic;
using Cook_Robert_SignInSignUp.Models;
using Xamarin.Forms;

namespace Cook_Robert_SignInSignUp
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            signInButton.Clicked += SignInButton_Clicked;
            signUpButton.Clicked += SignUpButton_Clicked;
            forgotPassButton.Clicked += ForgotPassButton_Clicked;
        }

        private void ForgotPassButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Hint", "Hint: Password", "Try Again");
            userNameEntry.Text = "";
            passwordEntry.Text = "";
        }

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        // Signs user in and verifys the user created an account by verifying username
        async void SignInButton_Clicked(object sender, EventArgs e)
        {
            
            UserData existingUser = UserData.GetUser(userNameEntry.Text);
            if (UserData.DoesUsernameExist(existingUser.UserName) == false)
            {
                bool answer = await DisplayAlert("Sorry!", "That username doesn't exist! Would you like to create an account?", "Yes", "No");
                if (answer == true)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    userNameEntry.Text = "";
                    passwordEntry.Text = "";
                }
            }
            else if (userNameEntry.Text != existingUser.UserName && passwordEntry.Text != existingUser.Password)
            {
                await DisplayAlert("Sorry!", "User/Password information is not correct", "Okay", "Close");
                userNameEntry.Text = "";
                passwordEntry.Text = "";
            }
            else if (userNameEntry.Text == null || passwordEntry.Text == null)
            {
                await DisplayAlert("Woah there!", "It seems you left a field blank. Please enter your username and password!", "Okay");
            }
        }
    }
}
