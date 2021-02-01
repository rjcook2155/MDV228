using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AllTheRickAndMorty
{
    public partial class TabbedPageMain : TabbedPage
    {
        public TabbedPageMain()
        {
            InitializeComponent();
            //Tabbed Pages initialized and icon image source set. 
            CharacterTab characterNavigation = new CharacterTab();
            characterNavigation.IconImageSource = "user.png";
            characterNavigation.Title = "Characters";
            

            EpisodeTab episodeNavigation = new EpisodeTab();
            episodeNavigation.IconImageSource = "film.png";
            episodeNavigation.Title = "Episodes";
            

            FavoritesTab favoriteNavigation = new FavoritesTab();
            favoriteNavigation.IconImageSource = "star.png";
            favoriteNavigation.Title = "Favorites";
            

            Children.Add(characterNavigation);
            Children.Add(episodeNavigation);
            Children.Add(favoriteNavigation);
            
        }
    }
}
