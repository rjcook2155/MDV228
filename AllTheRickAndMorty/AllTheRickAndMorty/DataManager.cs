using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AllTheRickAndMorty.Models;
using Newtonsoft.Json.Linq;

namespace AllTheRickAndMorty
{
    public class DataManager
    {
        // Manages API connection for character pull
        WebClient apiConnect = new WebClient();
        string newApi = "https://rickandmortyapi.com/api/character/?name=";
        string Name;
        string endAPI
        {
            get
            {
                return newApi + Name;
            }
        }

        List<CharacterInfo> downloadedCharacters = new List<CharacterInfo>();
        public DataManager(string nameToDownload)
        {
            Name = nameToDownload;
        }
        public async Task<List<CharacterInfo>> GetCharacter()
        {

            string apiString = await apiConnect.DownloadStringTaskAsync(endAPI);
            JObject jsonData = JObject.Parse(apiString);

            foreach (JObject results in jsonData["results"])
            {
                CharacterInfo newCharacter = new CharacterInfo();
                Debug.WriteLine(results["name"].ToString());
                newCharacter.CharName = results["name"].ToString();
                newCharacter.CharGender = results["gender"].ToString();
                newCharacter.CharSpecies = results["species"].ToString();
                newCharacter.CharStatus = results["status"].ToString();
                newCharacter.CharImage = results["image"].ToString();
                downloadedCharacters.Add(newCharacter);
            }

            return downloadedCharacters;
        }
    }
}
