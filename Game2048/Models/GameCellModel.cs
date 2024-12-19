using Game2048.Enums;
using Microsoft.VisualBasic;

namespace Game2048.Models
{
    public class GameCellModel
    {
        public CellType Type { get; private set; }
        public string Text { get; private set; }


        public GameCellModel(CellType type)
        {
            this.Type = type;
            this.Text = ((int)this.Type).ToString();
        }
    }
}
