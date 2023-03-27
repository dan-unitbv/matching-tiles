using System.Windows;

namespace MatchingTiles
{
    public partial class PlayGame : Window
    {
        private int rows = 4;
        private int columns = 4;
        private readonly User CurrentUser;
        public PlayGame(User user)
        {
            InitializeComponent();
            UserData userData = new UserData();
            CurrentUser = user;
        }

        private void OnFileNewGameClick(object sender, RoutedEventArgs e)
        {
            MatchingTiles memoryGame = new MatchingTiles(CurrentUser, rows, columns);
            memoryGame.ShowDialog();
        }

        private void OnFileOpenGameClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Not implemented yet.", "Error");
        }

        private void OnFileSaveGameClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Not implemented yet.", "Error");
        }

        private void OnFileStatisticsClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Not implemented yet.", "Error");
        }

        private void OnFileExitClick(object sender, RoutedEventArgs e)
        {
            SignIn startMenu = new SignIn();
            startMenu.Show();
            Close();
        }

        private void OnSettingsStandardClick(object sender, RoutedEventArgs e)
        {
            rows = 4;
            columns = 4;
            MessageBox.Show(this, $"You are playing with: 8 pairs of cards.", "Settings");
        }

        private void OnSettingsCustomClick(object sender, RoutedEventArgs e)
        {
            CustomGame customGame = new CustomGame();
            customGame.ShowDialog();
            rows = customGame.Rows;
            columns = customGame.Columns;

            if (rows != 0 && columns != 0)
            {
                MessageBox.Show(this, $"You are playing with: {(rows * columns) / 2} pairs of cards.", "Settings");
            }
        }

        private void OnHelpAboutClick(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }
}