using Location_guessing_game.Views;

namespace Location_guessing_game
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("registerpage", typeof(RegisterPage));
            Routing.RegisterRoute("mainpage", typeof(MainPage));
            Routing.RegisterRoute("gamepage", typeof(GamePage));
            Routing.RegisterRoute("selectlistpage", typeof(ListSelectPage));
            Routing.RegisterRoute("leaderboardpage", typeof(ListLeaderboardPage));
            // Everything except the one defined in AppShell.xaml
        }
    }
}
