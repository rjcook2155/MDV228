using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AllTheRickAndMorty
{
    public partial class App : Application
    {
        public static string FolderPath { get; internal set; }

        public App()
        {
            InitializeComponent();
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
