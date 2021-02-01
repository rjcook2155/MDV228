using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using AllTheRickAndMorty;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AllTheRickAndMorty.Models;
namespace AllTheRickAndMorty
{
    public class UserData
    {
        // User information as well as save/load users
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static List<UserData> AllUsers = new List<UserData>();

        public static List<UserData> GetUsers()
        {
            var files = Directory.EnumerateFiles(App.FolderPath, "*.data.txt");
            foreach (var filename in files)
            {
                 string[] lines = File.ReadAllLines(filename);

                 AllUsers.Add(new UserData
                 {
                     FirstName = lines[0],
                     LastName = lines[1],
                     UserName = lines[2],
                     Email = lines[3],
                     Password = lines[4],
                 }) ;            
            }
            return AllUsers;
        }
        public static void SaveUsers(UserData newUser)
        {
            string fileName = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.data.txt");
            File.WriteAllLines(fileName, new string[] {
                newUser.FirstName,
                newUser.LastName,
                newUser.UserName,
                newUser.Email,
                newUser.Password
            });
            AllUsers.Add(newUser);
        }
        public static bool DoesUsernameExist(string userName)
        {
            List<string> UserNames = (from user in AllUsers select user.UserName).ToList();

            return UserNames.Contains(userName);
        }
        public static bool DoesEmailExist(string userEmail)
        {
            List<string> userEmails = (from user in AllUsers select user.Email).ToList();
            return userEmails.Contains(userEmail);
        }
        public static UserData SignInValidation(string UserName)
        {
            UserData newUser = new UserData();

            return newUser;
        }
        public static UserData GetUser(string userName)
        {
            foreach (UserData user in AllUsers)
            {
                if (user.UserName == userName)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
