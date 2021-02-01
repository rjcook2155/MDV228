using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AllTheRickAndMorty.Models;
using Xamarin.Forms;

namespace AllTheRickAndMorty
{
    public partial class FavoritesTab : ContentPage
    {
        // Favorites tab brings in favorite characters and episodes and puts them in list view
        List<CharacterInfo> FavoriteChar = new List<CharacterInfo>();
        List<EpisodeInfo> favoriteEpi = new List<EpisodeInfo>();
        UserData currentUser = new UserData();
        public FavoritesTab()
        {
            InitializeComponent();
            LoadFavorites();
            MessagingCenter.Subscribe<List<CharacterInfo>>(this, "favoriteCharacter", (sender) =>
            {

                foreach (CharacterInfo character in sender)
                {
                    CharacterInfo favorite = new CharacterInfo();
                    favorite.CharImage = character.CharImage;
                    favorite.CharName = character.CharName;
                    favorite.CharGender = character.CharGender;
                    favorite.CharSpecies = character.CharSpecies;
                    favorite.CharStatus = character.CharStatus;
                    FavoriteChar.Add(favorite);
                }
            });
            MessagingCenter.Subscribe<EpisodeInfo>(this, "favoriteEpisode", (sender) =>
            {

                EpisodeInfo episode = new EpisodeInfo();
                episode.EpisodeName = sender.EpisodeName;
                episode.AirDate = sender.AirDate;
                favoriteEpi.Add(episode);

            });
            MessagingCenter.Subscribe<UserData>(this, "currentUser", (sender) =>
            {
                currentUser.UserName = sender.UserName;
            });
            ToolbarItem SignOut = new ToolbarItem
            {
                Text = "",
                IconImageSource = ImageSource.FromFile("logout.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
            };
            this.ToolbarItems.Add(SignOut);

            SignOut.Clicked += SignOut_Clicked;
            characterButton.Clicked += CharacterButton_Clicked;
            episodeButton.Clicked += EpisodeButton_Clicked;
            listView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (favoriteEpi.Contains(e.SelectedItem))
            {
                favoriteEpi.Remove((EpisodeInfo)e.SelectedItem);
            }
            else
            {
                FavoriteChar.Remove((CharacterInfo)e.SelectedItem);
            }
        }

        private async void SignOut_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Leaving so soon?", "Are you sure you want to log out?", "Yes", "No");
            if (answer == true)
            {
                string characterFileName = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.{currentUser.UserName}char.txt");
                foreach (CharacterInfo character in FavoriteChar)
                {
                    File.WriteAllLines(characterFileName, new string[] {
                        character.CharImage,
                        character.CharName,
                        character.CharGender,
                        character.CharSpecies,
                        character.CharStatus
            });
                }
                string episodeFileName = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.{currentUser.UserName}epi.txt");
                foreach (EpisodeInfo episode in favoriteEpi)
                {
                    File.WriteAllLines(episodeFileName, new string[] {
                        episode.EpisodeName,
                        episode.AirDate
            });


                }
                await Navigation.PushAsync(new MainPage());
            }
        }

        private void EpisodeButton_Clicked(object senders, EventArgs e)
        {

            DataTemplate dt = new DataTemplate(typeof(ImageCell));
            dt.SetBinding(ImageCell.TextProperty, new Binding("EpisodeName"));
            dt.SetBinding(ImageCell.DetailProperty, new Binding("AirDate"));
            dt.SetValue(ImageCell.TextColorProperty, Color.Blue);

            listView.ItemTemplate = dt;
            listView.ItemsSource = favoriteEpi;
        }

        private void CharacterButton_Clicked(object senders, EventArgs e)
        {

            DataTemplate dt = new DataTemplate(typeof(ImageCell));
            dt.SetBinding(ImageCell.ImageSourceProperty, new Binding("CharImage"));
            dt.SetBinding(ImageCell.TextProperty, new Binding("CharName"));
            dt.SetBinding(ImageCell.DetailProperty, new Binding("CharStatus"));

            dt.SetValue(ImageCell.TextColorProperty, Color.Blue);

            listView.ItemTemplate = dt;
            listView.ItemsSource = FavoriteChar;
        }
        public void LoadFavorites()
        {
            var charFiles = Directory.EnumerateFiles(App.FolderPath, "*.char.txt");
            foreach (var filename in charFiles)
            {
                string[] lines = File.ReadAllLines(filename);

                FavoriteChar.Add(new CharacterInfo
                {
                    CharImage = lines[0],
                    CharName = lines[1],
                    CharGender = lines[2],
                    CharSpecies = lines[3],
                    CharStatus = lines[4],

                });
            }
            
            var epiFiles = Directory.EnumerateFiles(App.FolderPath, "*.epi.txt");
            
            foreach (var filename in epiFiles)
            {
                File.Delete(filename);
                string[] lines = File.ReadAllLines(filename);

                favoriteEpi.Add(new EpisodeInfo
                {
                    EpisodeName = lines[0],
                    AirDate = lines[1],
                });
            }
        }
    }
}
