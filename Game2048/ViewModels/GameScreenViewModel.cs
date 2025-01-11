using Game2048.Models.Enums;
using Game2048.Models.Extensions;
using System.ComponentModel;

namespace Game2048.ViewModels
{
    public class GameScreenViewModel : INotifyPropertyChanged
    {
        private bool isGameOver;
        private string? text;
        private GameState state;
        private int score;

        public int Rows { get; set; }

        public int Cols { get; set; }

        public bool IsGameOver 
        { 
            get => isGameOver; 
            private set
            {
                isGameOver = value;
                this.OnPropertyChanged(nameof(IsGameOver));
            }
        }

        public string? Text 
        { 
            get => text; 
            private set 
            {
                text = value ?? string.Empty;
                this.OnPropertyChanged(nameof(Text));
            }
        }

        public GameState State
        {
            get => state;
            set 
            { 
                state = value;
                this.Text = state.GetDescription();
                this.IsGameOver = state != GameState.Running;
                this.OnPropertyChanged(nameof(State));
            }
        }

        public int Score
        {
            get => score;
            set 
            { 
                score = value;
                this.OnPropertyChanged(nameof(Score));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
