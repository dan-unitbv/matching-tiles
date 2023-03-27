using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MatchingTiles
{
    public partial class SignIn : Window
    {
        public delegate void RefreshListBox();
        public event RefreshListBox UsersListBoxRefreshed;

        private UserData UserData { get; set; }
        private User SelectedUser { get; set; }

        public SignIn()
        {
            InitializeComponent();
            UserData = new UserData();
        }

        private void UsersListBoxSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (UsersListBox.SelectedItem is User selected)
            {
                SelectedUser = selected;
                var uri = new Uri(SelectedUser.AvatarPath, UriKind.Relative);
                UserAvatar.Source = new BitmapImage(uri);
                UpdateDisabledButtons();
            }
        }

        private void OnNewUserButtonClick(object sender, RoutedEventArgs e)
        {
            var newUser = new NewUser();
            UsersListBoxRefreshed += UpdateUsersListBox;
            newUser.UpdateListOfUsers = UsersListBoxRefreshed;
            newUser.ShowDialog();
        }

        private void OnDeleteUserButtonClick(object sender, RoutedEventArgs e)
        {
            UserData.Users.Remove(SelectedUser);
            SelectedUser.DeleteUserFile();
            SelectedUser = null;
            UsersListBox.ItemsSource = UserData.Users;
            UsersListBox.Items.Refresh();
            UpdateDisabledButtons();
            UserAvatar.Source = null;
        }

        private void OnPlayGameButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show(this, "Please select a user.", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            var playGame = new PlayGame(SelectedUser);
            playGame.Show();
            Close();
        }

        private void OnExitGameButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateDisabledButtons()
        {
            PlayGameButton.IsEnabled = DeleteUserButton.IsEnabled = (SelectedUser != null);
        }

        private void UpdateUsersListBox()
        {
            UsersListBox.ItemsSource = UserData.Users;
            UsersListBox.Items.Refresh();
        }

        public void AddUserToUsersList(User user)
        {
            UserData.Users.Add(user);
            SelectedUser = user;
            UsersListBox.SelectedItem = SelectedUser;
        }
    }
}