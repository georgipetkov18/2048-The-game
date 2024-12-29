using Game2048.Models.Enums;
using Microsoft.Maui.Controls.Shapes;

namespace Game2048.Models.Views;

public partial class GameCell : ContentView
{
    private const int MARGIN = 5;
    private Border innerContent;

    public GameCell(GameCellModel gameCellModel)
    {
        innerContent = gameCellModel.Type != CellType.Empty ? new Border
        {
            Margin = MARGIN,
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

        Content = innerContent;
    }

    public async Task MoveAsync(SwipeDirection direction, int cellsCrossedX, int cellsCrossedY)
    {
        if (innerContent == null)
        {
            return;
        }

        var translateToX = direction switch
        {
            SwipeDirection.Left => -(innerContent.Width + 2 * MARGIN),
            SwipeDirection.Right => innerContent.Width + 2 * MARGIN,
            _ => 0
        };

        var translateToY = direction switch
        {
            SwipeDirection.Up => -(innerContent.Height + 2 * MARGIN),
            SwipeDirection.Down => innerContent.Height + 2 * MARGIN,
            _ => 0
        };
        //await this.Content.TranslateTo(cellsCrossedX * translateToX, cellsCrossedY * translateToY);
        await innerContent.TranslateTo(cellsCrossedX * translateToX, cellsCrossedY * translateToY, 150);
    }
}