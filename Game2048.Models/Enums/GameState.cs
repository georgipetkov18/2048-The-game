using System.ComponentModel;

namespace Game2048.Models.Enums
{
    public enum GameState
    {
        Running = 0,

        [Description("You won!")]
        Won = 1,

        [Description("Game over")]
        Lost = 2,
    }
}
