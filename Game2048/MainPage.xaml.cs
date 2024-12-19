using Game2048.Enums;
using Microsoft.Maui.Controls.Shapes;

namespace Game2048
{
    public partial class MainPage : ContentPage
    {
        private const int ROWS = 4;
        private const int COLS = 4;

        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new
            {
                Rows = ROWS,
                Cols = COLS
            };

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    GameGrid.SetAt(i, j, CellType.Empty);
                }
            }
        }

        public void OnSwiped(object sender, SwipedEventArgs e)
        {
            if (e.Direction == SwipeDirection.Left)
            {

            }
            else if (e.Direction == SwipeDirection.Right)
            {

            }
            else if (e.Direction == SwipeDirection.Up)
            {

            }
            else if (e.Direction == SwipeDirection.Down)
            {

            }
        }
    }

}
