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
    public partial class EpisodeTab : ContentPage
    {
        // Episode information uses api to pull all seasons seperately and displays to user
        public List<EpisodeInfo> SeasonOneList = new List<EpisodeInfo>();
        public List<EpisodeInfo> SeasonTwoList = new List<EpisodeInfo>();
        public List<EpisodeInfo> SeasonThreeList = new List<EpisodeInfo>();
        public List<EpisodeInfo> SeasonFourList = new List<EpisodeInfo>();
        public List<EpisodeInfo> FavoriteEpisodes = new List<EpisodeInfo>();

        public string episodeApi = "https://rickandmortyapi.com/api/episode/";

        public int[] SeasonOne = {1,2,3,4,5,6,7,8,9,10,11};
        public int[] SeasonTwo = {12,13,14,15,16,17,18,19,20,21 };
        public int[] SeasonThree = { 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
        public int[] SeasonFour = { 32, 33, 34, 35, 36, 37, 38, 39, 40, 41 };
        WebClient apiConnect = new WebClient();

        public EpisodeTab()
        {
            InitializeComponent();
            DataTemplate dt = new DataTemplate(typeof(ImageCell));
            dt.SetBinding(ImageCell.TextProperty, new Binding("EpisodeName"));
            dt.SetBinding(ImageCell.DetailProperty, new Binding("AirDate"));

            dt.SetValue(ImageCell.TextColorProperty, Color.Blue);

            listView.ItemTemplate = dt;
            listView.ItemSelected += ListView_ItemSelected;
            seasonOneButton.Clicked += SeasonOneButton_Clicked;
            seasonTwoButton.Clicked += SeasonTwoButton_Clicked;
            seasonThreeButton.Clicked += SeasonThreeButton_Clicked;
            seasonFourButton.Clicked += SeasonFourButton_Clicked;
            
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Add to Favorites?", "Are you sure you'd like to add episode to your favorites?", "Yes", "No");
            if (answer == true)
            {
                MessagingCenter.Send<EpisodeInfo>((EpisodeInfo)e.SelectedItem, "favoriteEpisode");
            }
        }

        private async void SeasonFourButton_Clicked(object sender, EventArgs e)
        {
            foreach (int i in SeasonFour) {
                string finalApi = episodeApi + i;
                string apiString = await apiConnect.DownloadStringTaskAsync(finalApi);
                JObject episodeData = JObject.Parse(apiString);

                    EpisodeInfo newEpisode = new EpisodeInfo();
                    newEpisode.EpisodeName = episodeData["name"].ToString();
                    newEpisode.AirDate = episodeData["air_date"].ToString();
                    SeasonFourList.Add(newEpisode);
                } 
            listView.ItemsSource = SeasonFourList;
        }

        private async void SeasonThreeButton_Clicked(object sender, EventArgs e)
        {
            foreach (int i in SeasonThree)
            {
                string finalApi = episodeApi + i;
                string apiString = await apiConnect.DownloadStringTaskAsync(finalApi);
                JObject episodeData = JObject.Parse(apiString);

                EpisodeInfo newEpisode = new EpisodeInfo();
                newEpisode.EpisodeName = episodeData["name"].ToString();
                newEpisode.AirDate = episodeData["air_date"].ToString();
                SeasonThreeList.Add(newEpisode);
            } 
            listView.ItemsSource = SeasonThreeList;
        }

        private async void SeasonTwoButton_Clicked(object sender, EventArgs e)
        {
            foreach (int i in SeasonTwo)
            {
                string finalApi = episodeApi + i;
                string apiString = await apiConnect.DownloadStringTaskAsync(finalApi);
                JObject episodeData = JObject.Parse(apiString);

                EpisodeInfo newEpisode = new EpisodeInfo();
                newEpisode.EpisodeName = episodeData["name"].ToString();
                newEpisode.AirDate = episodeData["air_date"].ToString();
                SeasonTwoList.Add(newEpisode);
            } 
            listView.ItemsSource = SeasonTwoList;
        }

        private async void SeasonOneButton_Clicked(object sender, EventArgs e)
        {
            foreach (int i in SeasonOne)
            {
                string finalApi = episodeApi + i;
                string apiString = await apiConnect.DownloadStringTaskAsync(finalApi);
                JObject episodeData = JObject.Parse(apiString);

                EpisodeInfo newEpisode = new EpisodeInfo();
                newEpisode.EpisodeName = episodeData["name"].ToString();
                newEpisode.AirDate = episodeData["air_date"].ToString();
                SeasonOneList.Add(newEpisode);
            } 
            listView.ItemsSource = SeasonOneList;
        }
    }
}

