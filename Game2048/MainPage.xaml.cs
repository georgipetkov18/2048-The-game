
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

            this.game = new GameModel(ROWS, COLS);

            this.GameGrid.SetGrid(this.game.Grid);
        }

        public async void OnSwiped(object sender, SwipedEventArgs e)
        {
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
                            while (updateAtCol > 0)
                            {
                                //await this.GameGrid[i, j].Move(e.Direction);
                                updateAtCol--;
                            }
                        }
                        else if (e.Direction == SwipeDirection.Right)
                        {
                            while (updateAtCol < this.game.Grid.GetLength(1) - 1)
                            {
                                //await this.GameGrid[i, j].Move(e.Direction);
                                updateAtCol++;
                            }
                        }
                        else if (e.Direction == SwipeDirection.Up)
                        {
                            while (updateAtRow > 0)
                            {
                                //await this.GameGrid[i, j].Move(e.Direction);
                                updateAtRow--;
                            }
                        }
                        else if (e.Direction == SwipeDirection.Down)
                        {
                            while (updateAtRow < this.game.Grid.GetLength(0) - 1)
                            {
                                //await this.GameGrid[i, j].Move(e.Direction);
                                updateAtRow++;
                            }
                        }
                        await this.GameGrid[i, j].Move(e.Direction, Math.Abs(j - updateAtCol), Math.Abs(i - updateAtRow));

                        this.game.UpdateAt(i, j, CellType.Empty);
                        this.GameGrid.SetAt(i, j, CellType.Empty);
                        this.game.UpdateAt(updateAtRow, updateAtCol, currentCell.Type);
                        this.GameGrid.SetAt(updateAtRow, updateAtCol, currentCell.Type);
                    }
                }
            }

            //this.ForceLayout();
        }
    }

}
