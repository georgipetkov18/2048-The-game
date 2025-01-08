
using Game2048.Models;
using Game2048.Models.Enums;

namespace Game2048
{
    public partial class MainPage : ContentPage
    {
        private const int ROWS = 4;
        private const int COLS = 4;

        private GameModel game;

        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new
            {
                Rows = ROWS,
                Cols = COLS
            };

            this.game = new GameModel(this.GameGrid, ROWS, COLS);
        }

        public async void OnSwiped(object sender, SwipedEventArgs e)
        {
            var tasks = new List<Task>();

            if (e.Direction == SwipeDirection.Left || e.Direction == SwipeDirection.Up)
            {
                for (int i = 0; i < this.game.Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < this.game.Grid.GetLength(1); j++)
                    {
                        var currentCell = this.game.Grid[i, j];
                        var nextCellType = currentCell.Type;

                        if (currentCell.Type != CellType.Empty)
                        {
                            var updateAtRow = i;
                            var updateAtCol = j;

                            if (e.Direction == SwipeDirection.Left)
                            {
                                while (updateAtCol > 0 && (this.game.Grid[i, updateAtCol - 1].Type == CellType.Empty || this.game.Grid[i, updateAtCol - 1].Type == nextCellType))
                                {
                                    this.PrepareCellType(updateAtRow, updateAtCol, i, updateAtCol - 1, ref nextCellType);
                                    updateAtCol--;
                                }
                            }

                            else if (e.Direction == SwipeDirection.Up)
                            {
                                while (updateAtRow > 0 && (this.game.Grid[updateAtRow - 1, j].Type == CellType.Empty || this.game.Grid[updateAtRow - 1, j].Type == nextCellType))
                                {
                                    this.PrepareCellType(updateAtRow, updateAtCol, updateAtRow - 1, j, ref nextCellType);
                                    updateAtRow--;
                                }
                            }

                            if (updateAtRow == i && updateAtCol == j)
                            {
                                continue;
                            }
                            this.game.Grid[i, j] = new GameCellModel(CellType.Empty);
                            this.game.Grid[updateAtRow, updateAtCol] = new GameCellModel(nextCellType);

                            tasks.Add(this.game.MoveCellAsync(e.Direction, nextCellType, i, updateAtRow, j, updateAtCol));
                        }
                    }
                }
            }

            else if (e.Direction == SwipeDirection.Right || e.Direction == SwipeDirection.Down)
            {
                for (int i = this.game.Grid.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = this.game.Grid.GetLength(1) - 1; j >= 0; j--)
                    {
                        var currentCell = this.game.Grid[i, j];
                        var nextCellType = currentCell.Type;

                        if (currentCell.Type != CellType.Empty)
                        {
                            var updateAtRow = i;
                            var updateAtCol = j;

                            if (e.Direction == SwipeDirection.Right)
                            {
                                while (updateAtCol < this.game.Grid.GetLength(1) - 1 && (this.game.Grid[i, updateAtCol + 1].Type == CellType.Empty || this.game.Grid[i, updateAtCol + 1].Type == nextCellType))
                                {
                                    this.PrepareCellType(updateAtRow, updateAtCol, i, updateAtCol + 1, ref nextCellType);
                                    updateAtCol++;
                                }
                            }

                            else if (e.Direction == SwipeDirection.Down)
                            {
                                while (updateAtRow < this.game.Grid.GetLength(0) - 1 && (this.game.Grid[updateAtRow + 1, j].Type == CellType.Empty || this.game.Grid[updateAtRow + 1, j].Type == nextCellType))
                                {
                                    this.PrepareCellType(updateAtRow, updateAtCol, updateAtRow + 1, j, ref nextCellType);
                                    updateAtRow++;
                                }
                            }

                            if (updateAtRow == i && updateAtCol == j)
                            {
                                continue;
                            }
                            this.game.Grid[i, j] = new GameCellModel(CellType.Empty);
                            this.game.Grid[updateAtRow, updateAtCol] = new GameCellModel(nextCellType);

                            tasks.Add(this.game.MoveCellAsync(e.Direction, nextCellType, i, updateAtRow, j, updateAtCol));
                        }
                    }
                }
            }
            await Task.WhenAll(tasks);
            this.game.CreateNewBaseCell();
        }

        private void PrepareCellType(int prevRow, int prevCol, int row, int col, ref CellType nextCellType)
        {
            if (this.game.Grid[row, col].Type == nextCellType)
            {
                var currentTypeValue = (int)this.game.Grid[row, col].Type;
                nextCellType = (CellType)(currentTypeValue * 2);
            }
        }
    }
}
