using Microsoft.Maui.Controls.Shapes;

namespace Game2048.Views;

public partial class GameCell : ContentView
{
    public string Text { get; private set; }

    public GameCell(string text)
    {
        this.Text = text;
        this.Content =
                new Border
                {
                    Stroke = Brush.LightGray,
                    Padding = 5,
                    StrokeThickness = 2,
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = new CornerRadius(10)
                    },
                    Content = new Border
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
                            Text = this.Text,
                        },
                    },
                };
    }
}