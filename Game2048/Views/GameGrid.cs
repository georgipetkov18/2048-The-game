using Game2048.Models;
using Game2048.Models.Enums;
using Microsoft.Maui.Controls.Shapes;

namespace Game2048.Views;

public partial class GameGrid : ContentView
{
    public static readonly BindableProperty RowsProperty =
        BindableProperty.Create(nameof(Rows), typeof(int), typeof(GameGrid), propertyChanged: OnGridSizeChanged);

    public static readonly BindableProperty ColsProperty =
        BindableProperty.Create(nameof(Cols), typeof(int), typeof(GameGrid), propertyChanged: OnGridSizeChanged);

    public int Rows
    {
        get => (int)GetValue(RowsProperty);
        set => SetValue(RowsProperty, value);
    }

    public int Cols
    {
        get => (int)GetValue(ColsProperty);
        set => SetValue(ColsProperty, value);
    }

    private Grid grid;

    private static void OnGridSizeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as GameGrid)?.SetupGrid();
    }

    public GameGrid()
    {
        this.grid = new Grid
        {
            HorizontalOptions = LayoutOptions.Fill,
        };

        this.SetupGrid();
    }

    private void SetupGrid()
    {
        this.grid.RowDefinitions.Clear();
        this.grid.ColumnDefinitions.Clear();

        for (int i = 0; i < Rows; i++)
        {
            this.grid.RowDefinitions.Add(new RowDefinition(100));
        }

        for (int i = 0; i < Cols; i++)
        {
            this.grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        this.Content = this.grid;
    }

    public void SetAt(int row, int col, CellType cellType)
    {
        var child = this.grid.Children
                .Where(c => this.grid.GetRow(c) == row && this.grid.GetColumn(c) == col)
                .FirstOrDefault();

        if (child != null)
        {
            this.grid.Children.Remove(child);
        }

        var gameCell = new GameCell(new GameCellModel(cellType));
        this.grid.Children.Add(gameCell);
        this.grid.SetRow(gameCell, row);
        this.grid.SetColumn(gameCell, col);

        this.Content = this.grid;
    }

    public void SetGrid(GameCellModel[,] gameGrid)
    {
        for (int i = 0; i < gameGrid.GetLength(0); i++)
        {
            for (int j = 0; j < gameGrid.GetLength(1); j++)
            {
                var gameCell = new GameCell(gameGrid[i, j]);
                var box = new Border 
                {
                    Stroke = Colors.LightGray,
                    StrokeThickness = 2,
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = new CornerRadius(10)
                    },
                };
                this.grid.Children.Add(gameCell);
                this.grid.Add(box);
                this.grid.SetRow(gameCell, i);
                this.grid.SetRow(box, i);
                this.grid.SetColumn(gameCell, j);
                this.grid.SetColumn(box, j);

                this.Content = this.grid;
            }
        }
    }

    public GameCell this[int i, int j]
    {
        get
        {
            return (GameCell?)this.grid.Children
                .Where(c => this.grid.GetRow(c) == i && this.grid.GetColumn(c) == j && c.GetType() == typeof(GameCell))
                .FirstOrDefault() ?? throw new IndexOutOfRangeException();
        }
    }
}