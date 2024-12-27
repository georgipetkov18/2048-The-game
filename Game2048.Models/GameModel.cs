using Game2048.Models.Enums;

namespace Game2048.Models
{
    public class GameModel
    {
        private readonly int rows;
        private readonly int cols;

        public GameCellModel[,] Grid { get; private set; }

        public GameModel(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;

            this.InitializeGrid();
        }

        public void InitializeGrid()
        {
            this.Grid = new GameCellModel[this.rows, this.cols];
            var chosenRow = Random.Shared.Next(this.rows);
            var chosenCol = Random.Shared.Next(this.cols);

            for (int i = 0; i < this.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < this.Grid.GetLength(1); j++)
                {
                    this.Grid[i, j] = new GameCellModel(i == chosenRow && j == chosenCol ? CellType.Type2 : CellType.Empty);
                }
            }
        }

        public void UpdateAt(int row, int col, CellType cellType)
        {
            this.Grid[row, col] = new GameCellModel(cellType);
        }
    }
}
