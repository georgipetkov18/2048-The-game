using Game2048.Models.Enums;
using Game2048.Models.Views;

namespace Game2048.Models
{
    public class GameModel
    {
        private readonly HashSet<int> usedCellIndexes;
        private readonly GameGrid gameGridView;
        private readonly int rowsCount;
        private readonly int colsCount;

        private List<int?> horizontalBorders;
        private List<int?> verticalBorders;

        public GameCellModel[,] Grid { get; private set; }

        public GameModel(GameGrid gameGridView, int rows, int cols)
        {
            this.usedCellIndexes = [];
            this.gameGridView = gameGridView;
            this.rowsCount = rows;
            this.colsCount = cols;

            this.ResetBorders();

            this.InitializeGrid();
        }

        public void InitializeGrid()
        {
            this.Grid = new GameCellModel[this.rowsCount, this.colsCount];
            var chosenRow = Random.Shared.Next(this.rowsCount);
            var chosenCol = Random.Shared.Next(this.colsCount);

            for (int i = 0; i < this.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < this.Grid.GetLength(1); j++)
                {
                    if (i == chosenRow && j == chosenCol)
                    {
                        this.usedCellIndexes.Add(this.GetIndex(i, j));
                        this.Grid[i, j] = new GameCellModel(CellType.Type2);
                    }
                    else
                    {
                        this.Grid[i, j] = new GameCellModel(CellType.Empty);
                    }
                }
            }

            this.gameGridView.SetGrid(this.Grid);
        }

        public void UpdateAt(int row, int col, CellType cellType)
        {
            this.Grid[row, col] = new GameCellModel(cellType);
            this.gameGridView.SetAt(row, col, cellType);

            if (cellType != CellType.Empty)
            {
                this.usedCellIndexes.Add(this.GetIndex(row, col));
            }

            else
            {
                this.usedCellIndexes.Remove(this.GetIndex(row, col));
            }
        }

        public void CreateNewBaseCell()
        {
            var generatedIndexes = new HashSet<int>();
            int cellIndex;
            do
            {
                cellIndex = Random.Shared.Next(this.rowsCount * this.colsCount);
                generatedIndexes.Add(cellIndex);

                if (generatedIndexes.Count == this.rowsCount * this.colsCount)
                {
                    return;
                }

            }
            while (this.usedCellIndexes.Contains(cellIndex));

            var gridPosition = this.GetGridPosition(cellIndex);

            this.UpdateAt(gridPosition.Item1, gridPosition.Item2, CellType.Type2);
        }

        public bool IsGameOver()
        {
            if (this.usedCellIndexes.Count != this.rowsCount * this.colsCount)
            {
                return false;
            }

            for (int i = 0; i < this.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < this.Grid.GetLength(1); j++)
                {
                    var currentCellType = this.Grid[i, j].Type;

                    if ((i > 0 && this.Grid[i - 1, j].Type == currentCellType) ||
                        (j < this.Grid.GetLength(1) - 1 && this.Grid[i, j + 1].Type == currentCellType) ||
                        (i < this.Grid.GetLength(0) - 1 && this.Grid[i + 1, j].Type == currentCellType) ||
                        (j > 0 && this.Grid[i, j - 1].Type == currentCellType))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task MoveCellAsync(SwipeDirection direction, CellType cellType, int fromRow, int toRow, int fromCol, int toCol)
        {
            await this.gameGridView[fromRow, fromCol].MoveAsync(direction, Math.Abs(fromCol - toCol), Math.Abs(fromRow - toRow));
            this.UpdateAt(fromRow, fromCol, CellType.Empty);
            this.UpdateAt(toRow, toCol, cellType);
        }

        public void ResetBorders()
        {
            this.horizontalBorders = Enumerable.Repeat<int?>(null, this.rowsCount).ToList();
            this.verticalBorders = Enumerable.Repeat<int?>(null, this.colsCount).ToList();
        }

        public int? GetHorizontalBorder(int col) => this.horizontalBorders[col];

        public int? GetVerticalBorder(int row) => this.verticalBorders[row];

        public void SetHorizontalBorder(int col, int value)
        {
            this.horizontalBorders[col] = value;
        }

        public void SetVerticalBorder(int row, int value)
        {
            this.verticalBorders[row] = value;
        }

        private int GetIndex(int row, int col) => this.colsCount * row + col;

        private Tuple<int, int> GetGridPosition(int index) => new(index / this.colsCount, index % this.colsCount);
    }
}
