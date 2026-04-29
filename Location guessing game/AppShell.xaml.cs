namespace Location_guessing_game
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("gamepage", typeof(Views.GamePage));
        }
    }
}
