
using Game2048.DataAccess.Repositories;
using Game2048.Models;
using Game2048.Models.Enums;
using Game2048.Models.Extensions;
using Game2048.ViewModels;

namespace Game2048
{
    public partial class MainPage : ContentPage
    {
        private const int ROWS = 4;
        private const int COLS = 4;
        private readonly ScoreRepository scoreRepository;
        private GameState gameState;

        private GameModel game;

        public MainPage(ScoreRepository scoreRepository)
        {
            InitializeComponent();
            this.BindingContext = new GameScreenViewModel
            {
                Rows = ROWS,
                Cols = COLS,
                IsGameOver = false
            };

            this.gameState = GameState.Running;
            this.game = new GameModel(this.GameGrid, ROWS, COLS);
            this.scoreRepository = scoreRepository;
        }

        public async void OnSwiped(object sender, SwipedEventArgs e)
        {
            if (this.gameState != GameState.Running)
            {
                return;
            }

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

                            if (nextCellType == CellType.Type2048)
                            {
                                // TODO: Add proper score to repository
                                await scoreRepository.SaveScoreAsync(new DataAccess.Entities.Score { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Moves = 0, Points = 2048 });
                                this.SwitchGameState(GameState.Won);
                            }

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

                            if (nextCellType == CellType.Type2048)
                            {
                                // TODO: Add proper score to repository
                                await scoreRepository.SaveScoreAsync(new DataAccess.Entities.Score { Id = Guid.NewGuid(), CreatedOn = DateTime.Now, Moves = 0, Points = 2048 });
                                this.SwitchGameState(GameState.Won);
                            }

                            tasks.Add(this.game.MoveCellAsync(e.Direction, nextCellType, i, updateAtRow, j, updateAtCol));
                        }
                    }
                }
            }

            await Task.WhenAll(tasks);
            var canCreateNewCell = this.game.CreateNewBaseCell();

            if (!canCreateNewCell)
            {
                this.SwitchGameState(GameState.Lost);
            }
        }

        private void PrepareCellType(int prevRow, int prevCol, int row, int col, ref CellType nextCellType)
        {
            if (this.game.Grid[row, col].Type == nextCellType)
            {
                var currentTypeValue = (int)this.game.Grid[row, col].Type;
                nextCellType = (CellType)(currentTypeValue * 2);
            }
        }
        private void OnNewGameBtnClicked(object sender, EventArgs e)
        {
            this.GameGrid.InitializeGrid();
            this.game = new GameModel(this.GameGrid, ROWS, COLS);
            this.SwitchGameState(GameState.Running);
        }

        private void SwitchGameState(GameState state)
        {
            this.gameState = state;
            this.BindingContext = new GameScreenViewModel
            {
                Rows = ROWS,
                Cols = COLS,
                IsGameOver = state != GameState.Running,
                Text = state.GetDescription(),
            };
        }
    }
}
