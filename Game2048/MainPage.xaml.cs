
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

            for (int i = 0; i < this.game.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < this.game.Grid.GetLength(1); j++)
                {
                    var currentCell = this.game.Grid[i, j];

                    if (currentCell.Type != CellType.Empty)
                    {
                        var updateAtRow = i;
                        var updateAtCol = j;

                        if (e.Direction == SwipeDirection.Left)
                        {
                            // NOT FINAL CONDITION NEED TO CHECK FOR ALL TYPES EXCEPT ITS OWN AND IF IT'S ITS OWN NEED TO MERGE
                            while (updateAtCol > 0 && this.game.Grid[i, updateAtCol - 1].Type == CellType.Empty)
                            {
                                //await this.GameGrid[i, j].Move(e.Direction);
                                updateAtCol--;
                            }
                        }
                        else if (e.Direction == SwipeDirection.Right)
                        {
                            while (updateAtCol < this.game.Grid.GetLength(1) - 1 && this.game.Grid[i, updateAtCol + 1].Type == CellType.Empty)
                            {
                                //await this.GameGrid[i, j].Move(e.Direction);
                                updateAtCol++;
                            }
                        }
                        else if (e.Direction == SwipeDirection.Up)
                        {
                            while (updateAtRow > 0 && this.game.Grid[updateAtRow - 1, j].Type == CellType.Empty)
                            {
                                //await this.GameGrid[i, j].Move(e.Direction);
                                updateAtRow--;
                            }
                        }
                        else if (e.Direction == SwipeDirection.Down)
                        {
                            while (updateAtRow < this.game.Grid.GetLength(0) - 1 && this.game.Grid[updateAtRow + 1, j].Type == CellType.Empty)
                            {
                                //await this.GameGrid[i, j].Move(e.Direction);
                                updateAtRow++;
                            }
                        }

                        if (updateAtRow == i && updateAtCol == j)
                        {
                            continue;
                        }

                        tasks.Add(this.game.MoveCellAsync(e.Direction, currentCell.Type, i, updateAtRow, j, updateAtCol));

                        //tasks.Add(this.GameGrid[i, j].MoveAsync(e.Direction, Math.Abs(j - updateAtCol), Math.Abs(i - updateAtRow)).ContinueWith((t) =>
                        //{
                        //    this.game.UpdateAt(i, j, CellType.Empty);
                        //    this.game.UpdateAt(updateAtRow, updateAtCol, currentCell.Type);
                        //}));
                        //await this.GameGrid[i, j].MoveAsync(e.Direction, Math.Abs(j - updateAtCol), Math.Abs(i - updateAtRow));

                        //this.game.UpdateAt(i, j, CellType.Empty);
                        //this.game.UpdateAt(updateAtRow, updateAtCol, currentCell.Type);
                    }
                }
            }
            await Task.WhenAll(tasks);
            this.game.CreateNewBaseCell();

            //this.ForceLayout();
        }
    }

}
