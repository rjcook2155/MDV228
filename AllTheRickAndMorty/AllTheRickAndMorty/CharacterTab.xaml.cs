using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Net;
using AllTheRickAndMorty.Models;


namespace AllTheRickAndMorty
{ 
    public partial class CharacterTab : ContentPage
    {
        List<CharacterInfo> allCharacters = new List<CharacterInfo>();

        // Pulls character information and displays all relevent characters to user. 
        public CharacterTab()
        {
            InitializeComponent();

            DataTemplate dt = new DataTemplate(typeof(ImageCell));
            dt.SetBinding(ImageCell.ImageSourceProperty, new Binding("CharImage"));
            dt.SetBinding(ImageCell.TextProperty, new Binding("CharName"));
            dt.SetBinding(ImageCell.DetailProperty, new Binding("CharStatus"));

            dt.SetValue(ImageCell.TextColorProperty, Color.Blue);

            listView.ItemTemplate = dt;

            findButton.Clicked += FindButton_Clicked;
            listView.ItemSelected += ListView_ItemSelected;
    }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new CharacterModal());

            MessagingCenter.Send<CharacterInfo>((CharacterInfo)e.SelectedItem, "CharacterDisplay");
        }

        async void FindButton_Clicked(object sender, EventArgs e)
    {
            if (nameEditor.Text == null)
            {
                await DisplayAlert("Woah there!", "Please enter a name before continuing!", "Okay");
            }
            else {
                DataManager _dataManager = new DataManager(nameEditor.Text);
                allCharacters = await _dataManager.GetCharacter();
                listView.ItemsSource = allCharacters;
            }
    }
}
}