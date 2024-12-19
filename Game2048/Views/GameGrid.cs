using Game2048.Enums;

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
            HorizontalOptions = LayoutOptions.Fill
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
        if (cellType == CellType.Empty)
        {
            this.RemoveAt(row, col);
            return;
        }

        var gameCell = new GameCell(((int)cellType).ToString());
        this.grid.Children.Add(gameCell);
        this.grid.SetRow(gameCell, row);
        this.grid.SetColumn(gameCell, col);

        this.Content = this.grid;
    }

    private void RemoveAt(int row, int col)
    {
        foreach (var child in this.grid.Children.ToList())
        {
            if (this.grid.GetRow(child) == row && this.grid.GetColumn(child) == col)
            {
                this.grid.Children.Remove(child);
                break;
            }
        }
    }
}