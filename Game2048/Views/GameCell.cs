using Game2048.Enums;
using Game2048.Models;
using Microsoft.Maui.Controls.Shapes;

namespace Game2048.Views;

public partial class GameCell : ContentView
{
    public GameCell(GameCellModel gameCellModel)
    {
        this.Content = new Border
        {
            Stroke = Brush.LightGray,
            Padding = 5,
            StrokeThickness = 2,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(10)
            },
            Content = gameCellModel.Type != CellType.Empty ? new Border
            {
                BackgroundColor = Colors.Aquamarine,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(10)
                },
                Content = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = Colors.Black,
                    FontSize = 25,
                    Text = gameCellModel.Text,
                },
            } : null,
        };
    }
}