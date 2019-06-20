using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApplication
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MapManager Mapper = new MapManager();
        private static Button[,] MatrixOfMap = new Button[4, 4];
        
        public MainWindow()
        {
            InitializeComponent();
            MapInit(Mapper);
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
           
            var vKey = e.Key;
            switch (vKey)
            {
                case Key.Up:
                    MoveUp();                   
                    break;
                case Key.Down:
                    MoveDown();                   
                    break;
                case Key.Left:
                    MoveLeft();
                    break;
                case Key.Right:
                    MoveRight();    
                    break;
            }
        }
      
  

        private (int x, int y) GetPositionOfEmpty() => ((Int32)(EmptyRect.GetValue(Grid.RowProperty)),(Int32)(EmptyRect.GetValue(Grid.ColumnProperty)));

        
        private Button GetUIElementByRC(int posR, int posC)
        {
            UIElement uiElement = null;
            foreach(UIElement element in tgMap.Children)
            {
                if((int)element.GetValue(Grid.RowProperty)==posR && (int)element.GetValue(Grid.ColumnProperty) == posC)//
                {
                    uiElement = element;
                    break;
                } 
            }
            return uiElement as Button;
        }

        private void MoveUp()
        {
            var ps0 = GetPositionOfEmpty();
            var BtnToSwap = GetUIElementByRC(ps0.x - 1, ps0.y);
            Move(EmptyRect, BtnToSwap);
        }

        private void MoveDown()
        {
            var ps1 = GetPositionOfEmpty();
            var BtnToSwap = GetUIElementByRC(ps1.x + 1, ps1.y);
            Move(EmptyRect, BtnToSwap);
        }
        private void MoveLeft()
        {
            var ps2 = GetPositionOfEmpty();
            var BtnToSwap = GetUIElementByRC(ps2.x, ps2.y - 1);
            Move(EmptyRect, BtnToSwap);
        }
        private void MoveRight()
        {
            var ps3 = GetPositionOfEmpty();
            var BtnToSwap = GetUIElementByRC(ps3.x, ps3.y + 1);
            Move(EmptyRect, BtnToSwap);
        }



        private static void Move(Rectangle EmptyRect, Button ToSwapBtn)
        {
            (int x, int y) bp = ((int)ToSwapBtn.GetValue(Grid.RowProperty),(int)ToSwapBtn.GetValue(Grid.ColumnProperty));
            (int x, int y) rp= ((int)EmptyRect.GetValue(Grid.RowProperty), (int)EmptyRect.GetValue(Grid.ColumnProperty));
            EmptyRect.SetValue(Grid.RowProperty, bp.x);
            EmptyRect.SetValue(Grid.ColumnProperty, bp.y);
            ToSwapBtn.SetValue(Grid.RowProperty, rp.x);
            ToSwapBtn.SetValue(Grid.ColumnProperty, rp.y);
        }


        private void MapInit(MapManager Mapper)
        {
            int k = 0;
            var Initial = Mapper.Init();

            
            for (int i = 0; i < 15; i++)
            {
                if (i % 4 == 0 && i > 0)
                {
                    k++;
                    MatrixOfMap[k, i % 4] = tgMap.Children[i] as Button;
                }
                else
                {
                    MatrixOfMap[k, i % 4] = tgMap.Children[i] as Button;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == 3 && j == 3) { break; }
                    MatrixOfMap[i, j].Content = Initial[i, j];
                }
            }
           
        }

    }
}
