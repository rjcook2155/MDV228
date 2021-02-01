using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AllTheRickAndMorty
{
    public partial class SignInPage : ContentPage
    {
        // Sign in page takes in username and password. Verifies each matches before letting user past
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

            UserData currentUser = UserData.GetUser(userNameEntry.Text);

            while (userNameEntry.Text == null || passwordEntry.Text == null)
            {
                await DisplayAlert("Woah there!", "It seems you left a field blank. Please enter your username and password!", "Okay");
            }

            if (UserData.DoesUsernameExist(currentUser.UserName) == false)
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
            else if (userNameEntry.Text != currentUser.UserName && passwordEntry.Text != currentUser.Password)
            {
                await DisplayAlert("Sorry!", "User/Password information is not correct", "Okay", "Close");
                userNameEntry.Text = "";
                passwordEntry.Text = "";
            }
            else
            {
                MessagingCenter.Send<UserData>(currentUser, "currentUser");
                await Navigation.PushAsync(new TabbedPageMain());
            }
        }
    }
}
