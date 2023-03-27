using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace MatchingTiles
{
    public class UserData
    {
        public List<User> Users { get; set; }
        public static List<string> AvatarsPaths { get; private set; }

        public UserData()
        {
            Users = new List<User>();
            GetUsersFromDirectory();
        }

        static UserData()
        {
            AvatarsPaths = new List<string>();
            LoadAvatarsPaths();
        }

        private void GetUsersFromDirectory()
        {
            var usersDirectory = Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\Users");
            var userFilesPaths = Directory.GetFiles(usersDirectory).Where(x => x != Path.Combine(usersDirectory, ".gitkeep"));

            foreach (var userFile in userFilesPaths)
            {
                using (StreamReader streamReader = new StreamReader(userFile))
                {
                    User user = new User(streamReader.ReadLine(), streamReader.ReadLine(), Guid.Parse(streamReader.ReadLine()));
                    Users.Add(user);
                }
            }
        }

        public static void LoadAvatarsPaths()
        {
            AvatarsPaths = Directory.GetFiles(@"../../Resources/Avatars/").Select(x => "/MatchingTiles;component" + x.Substring(5)).ToList();
        }
    }
}