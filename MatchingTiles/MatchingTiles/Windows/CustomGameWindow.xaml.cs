using System.Windows;

namespace MatchingTiles
{
    public partial class CustomGame : Window
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public CustomGame()
        {
            InitializeComponent();
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(RowsComboBox.Text) || string.IsNullOrEmpty(ColumnsComboBox.Text))
            {
                MessageBox.Show(this, "Please select the settings for your custom game.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool rowsParsed = int.TryParse(RowsComboBox.Text, out int rows);
            bool columnsParsed = int.TryParse(ColumnsComboBox.Text, out int columns);

            if (rows * columns % 2 == 1)
            {
                MessageBox.Show(this, "Please select the settings so that the number of cards is even.", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            Rows = rows;
            Columns = columns;

            Close();
        }
    }
}