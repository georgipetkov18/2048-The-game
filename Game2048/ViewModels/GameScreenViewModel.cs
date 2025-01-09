namespace Game2048.ViewModels
{
    public class GameScreenViewModel
    {
        public int Rows { get; set; }
        public int Cols { get; set; }

        public bool IsGameOver { get; set; }

        public string Text { get; set; }
    }
}
