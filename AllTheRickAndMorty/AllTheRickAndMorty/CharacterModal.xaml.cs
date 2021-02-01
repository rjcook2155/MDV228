using System;
using System.Collections.Generic;
using AllTheRickAndMorty.Models;
using Xamarin.Forms;

namespace AllTheRickAndMorty
{
    public partial class CharacterModal : ContentPage
    {
        List<CharacterInfo> FavoriteCharacters = new List<CharacterInfo>();
        public CharacterModal()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<CharacterInfo>(this, "CharacterDisplay", (sender) =>
            {
                CharacterInfo newChar = sender;

                characterImage.Source = sender.CharImage.ToString();
                characterName.Text = "Name: " + sender.CharName.ToString();
                characterGender.Text = "Gender: " + sender.CharGender.ToString();
                characterSpecies.Text = "Species: " + sender.CharSpecies.ToString();
                characterStatus.Text = "Status: " + sender.CharStatus.ToString();
                if (sender.CharStatus.ToString().ToLower() == "alive")
                {
                    characterStatus.TextColor = Color.DarkGreen;
                }
                else
                {
                    characterStatus.TextColor = Color.DarkRed;
                }
                FavoriteCharacters.Add(newChar);
            });
            
            ToolbarItem favorite = new ToolbarItem
            {
                Text = "",
                IconImageSource = ImageSource.FromFile("star.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
            };
            this.ToolbarItems.Add(favorite);

            favorite.Clicked += Favorite_Clicked;
        }

        private void Favorite_Clicked(object sender, EventArgs e)
        {
            ToolbarItem item = (ToolbarItem)sender;
            item.IconImageSource = ImageSource.FromFile("star2.png");
            MessagingCenter.Send<List<CharacterInfo>>(FavoriteCharacters, "favoriteCharacter");
        }

    }
}
