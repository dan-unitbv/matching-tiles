using System;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;

namespace MatchingTiles
{
    public partial class MatchingTiles : Window
    {
        User CurrentUser;

        List<KeyValuePair<Button, Button>> Tiles { get; set; }
        List<List<string>> TilesLevelUp { get; set; }
        List<KeyValuePair<Button, Button>> Buttons { get; set; }
        private GameBoard GameBoard { get; set; }

        int CurrentLevel = 1;

        public MatchingTiles()
        {
            TileData.LoadCards();
        }

        public MatchingTiles(User selectedUser, int rows, int columns)
        {
            CurrentUser = selectedUser;

            Tiles = new List<KeyValuePair<Button, Button>>();
            TilesLevelUp = TileData.TilesPaths;
            Buttons = new List<KeyValuePair<Button, Button>>();
            GameBoard = new GameBoard(rows, columns);

            InitializeComponent();

            TileData.LoadCards();

            CurrentLevelLabel.Content = CurrentLevel;
        }

        private void OnFileNewGameClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "You are already in a game!", "Error");
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
            MessageBox.Show(this, "Not available while in a game!", "Error");
        }

        private void OnSettingsCustomClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Not available while in a game!", "Error");
        }

        private void OnHelpAboutClick(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void OnTileButtonClick(object sender, RoutedEventArgs e)
        {
            if (Tiles.Count == 2)
            {
                return;
            }

            Button clickedButton = sender as Button;

            FrameworkElement parent = clickedButton.Parent as FrameworkElement;
            Button cardButton = parent.FindName("TileButton") as Button;

            if (cardButton is null)
            {
                return;
            }

            Tiles.Add(new KeyValuePair<Button, Button>(clickedButton, cardButton));

            cardButton.Visibility = Visibility.Visible;

            CheckBoard();
        }

        private void OnNextLevelButtonClick(object sender, RoutedEventArgs e)
        {
            if (TilesLevelUp == null)
            {
                TileData.LoadCards();
                TilesLevelUp = TileData.TilesPaths;
            }

            if (TilesLevelUp != null && Buttons.Count != TilesLevelUp.Count * TilesLevelUp[0].Count)
            {
                MessageBox.Show(this, "Please finish the current level first.", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            if (CurrentLevel >= 3)
            {
                MessageBox.Show(this, "Congratulations! You won!", "Win", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                Close();
                return;
            }

            LevelUp();
        }

        private async void CheckBoard()
        {
            await Task.Delay(TimeSpan.FromSeconds(1.5));

            if (Tiles.Count != 2)
            {
                return;
            }

            if (GameBoard.AreMatching(Tiles))
            {
                GameBoard.HideIfMatching(Tiles);
                Buttons.AddRange(Tiles);
                TileData.TilesPaths = TileData.TilesPaths.Select(x => x.Where(card => card != Tiles[0].Value.DataContext as string).ToList()).ToList();
            }
            else
            {
                GameBoard.HideIfNotMatching(Tiles);
            }

            Tiles.RemoveRange(0, 2);
        }

        private void LevelUp()
        {
            if (Buttons.Count == 0)
            {
                return;
            }

            CurrentLevel++;
            CurrentLevelLabel.Content = CurrentLevel;

            Random random = new Random();
            var TilesPaths = TilesLevelUp;
            
            foreach (var pair in Buttons)
            {
                pair.Key.Visibility = Visibility.Visible;
                Panel.SetZIndex(pair.Key, 0);
                Panel.SetZIndex(pair.Value, 0);
                pair.Value.Visibility = Visibility.Collapsed;
            }

            Shuffles.ShuffleBoard(random, TilesLevelUp);
            TileData.TilesPaths = TilesLevelUp;
            CardItemsControl.ItemsSource = TilesLevelUp;
            CardItemsControl.Items.Refresh();
            Buttons.Clear();
        }
    }
}