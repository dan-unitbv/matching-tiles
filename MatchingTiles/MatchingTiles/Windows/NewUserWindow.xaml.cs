using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MatchingTiles
{
    public partial class NewUser : Window
    {
        private int CurrentAvatar;
        public Delegate UpdateListOfUsers;
        public NewUser()
        {
            InitializeComponent();
            AvatarImage.Source = new BitmapImage(new Uri(UserData.AvatarsPaths[CurrentAvatar], uriKind: UriKind.Relative));
        }

        private void OnNextAvatarButtonClick(object sender, RoutedEventArgs e)
        {
            if (CurrentAvatar + 1 > UserData.AvatarsPaths.Count - 1)
            {
                CurrentAvatar = 0;
            }
            else
            {
                CurrentAvatar++;
            }

            AvatarImage.Source = new BitmapImage(new Uri(UserData.AvatarsPaths[CurrentAvatar], uriKind: UriKind.Relative));
        }

        private void OnPreviousAvatarButtonClick(object sender, RoutedEventArgs e)
        {
            if (CurrentAvatar - 1 < 0)
            {
                CurrentAvatar = UserData.AvatarsPaths.Count - 1;
            }
            else
            {
                CurrentAvatar--;
            }

            AvatarImage.Source = new BitmapImage(new Uri(UserData.AvatarsPaths[CurrentAvatar], uriKind: UriKind.Relative));
        }

        private void OnCreateProfileButtonClick(object sender, RoutedEventArgs e)
        {
            CreateUserProfile();
        }

        private void CreateUserProfile()
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show(this, "Please enter your name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Guid guid = Guid.NewGuid();
            User newUser = new User(NameTextBox.Text, UserData.AvatarsPaths[CurrentAvatar], guid);
            newUser.CreateUserFile();

            SignIn startMenu = Application.Current.MainWindow as SignIn;
            startMenu.AddUserToUsersList(newUser);

            UpdateListOfUsers.DynamicInvoke();
            MessageBox.Show(this, "User created successfully. Enjoy the game!", "User", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}