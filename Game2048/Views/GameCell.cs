using Game2048.Models;
using Game2048.Models.Enums;
using Microsoft.Maui.Controls.Shapes;

namespace Game2048.Views;

public partial class GameCell : ContentView
{
    private const int PADDING = 5;
    private Border? innerContent;

    public GameCell(GameCellModel gameCellModel)
    {
        this.innerContent = gameCellModel.Type != CellType.Empty ? new Border
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
        } : null;

        this.Content = new Border
        {
            Stroke = Brush.LightGray,
            Padding = PADDING,
            StrokeThickness = 2,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(10)
            },
            Content = this.innerContent,
        };
    }

    public async Task Move(SwipeDirection direction, int cellsCrossedX, int cellsCrossedY)
    {
        if (this.innerContent == null)
        {
            return;
            
        }

        var translateToX = direction switch
        {
            SwipeDirection.Left => -(this.innerContent.Width + (2 * PADDING)),
            SwipeDirection.Right => this.innerContent.Width + (2 * PADDING),
            _ => 0
        };

        var translateToY = direction switch
        {
            SwipeDirection.Up => -(this.innerContent.Height + (2 * PADDING)),
            SwipeDirection.Down => this.innerContent.Height + (2 * PADDING),
            _ => 0
        };
        //await this.Content.TranslateTo(cellsCrossedX * translateToX, cellsCrossedY * translateToY);
        await this.innerContent.TranslateTo(cellsCrossedX * translateToX, cellsCrossedY * translateToY);
    }
}