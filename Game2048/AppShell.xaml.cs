namespace Game2048
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("ScoreboardPage", typeof(ScoreboardPage));
        }
    }
}
