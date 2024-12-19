using Microsoft.Maui.Controls.Shapes;

namespace Game2048
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            GameGrid.SetAt(0, 0, Enums.CellType.Type2);
            GameGrid.SetAt(0, 1, Enums.CellType.Type4);
            GameGrid.SetAt(0, 2, Enums.CellType.Type8);
            GameGrid.SetAt(0, 3, Enums.CellType.Type16);

            GameGrid.SetAt(1, 0, Enums.CellType.Type32);
            GameGrid.SetAt(1, 1, Enums.CellType.Type64);
            GameGrid.SetAt(1, 2, Enums.CellType.Type128);
            GameGrid.SetAt(1, 3, Enums.CellType.Type256);

            GameGrid.SetAt(2, 0, Enums.CellType.Empty);
            GameGrid.SetAt(1, 0, Enums.CellType.Empty);
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
