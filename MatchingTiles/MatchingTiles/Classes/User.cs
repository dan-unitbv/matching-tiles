using System;
using System.IO;
using System.Linq;

namespace MatchingTiles
{
    public class User
    {
        public string Username { get; private set; }
        public string AvatarPath { get; private set; }
        public Guid Guid { get; private set; }

        public User(string username, string avatarPath, Guid guid)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(avatarPath)) throw new ArgumentException(nameof(avatarPath));

            Username = username;
            AvatarPath = avatarPath;
            Guid = guid;
        }

        public void CreateUserFile()
        {
            string fileToCreate = $"../../Resources/Users/user-{Username}-{Guid}.txt";

            if (File.Exists(fileToCreate))
            {
                File.Delete(fileToCreate);
            }

            using (StreamWriter streamWriter = File.CreateText(fileToCreate))
            {
                streamWriter.WriteLine(Username);
                streamWriter.WriteLine(AvatarPath);
                streamWriter.WriteLine(Guid);
            }
        }

        public void DeleteUserFile()
        {
            var fileToDelete = Directory.GetFiles(@"../../Resources/Users/")
                .Where(x => x.Contains($"user-{Username}-{Guid}")).FirstOrDefault();

            if (fileToDelete is null)
            {
                return;
            }

            if (File.Exists(fileToDelete))
            {
                File.Delete(fileToDelete);
            }
        }
    }
}