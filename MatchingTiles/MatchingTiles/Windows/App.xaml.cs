using System;
using System.Windows;

namespace MatchingTiles
{
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            App application = new App();

            application.InitializeComponent();

            application.Run();
        }
    }
}
