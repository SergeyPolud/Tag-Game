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
using System.Windows.Threading;

namespace WpfApplication
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MapManager Mapper = new MapManager();
        private static Button[,] MatrixOfMap = new Button[4, 4];
        private static int[,] Initial = new int[4, 4];
        private static TimeSpan timeSpan = new TimeSpan(0,0,0);
        private static DispatcherTimer Tim1 = new DispatcherTimer();
        public MainWindow()
        {
            
            Tim1.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Tim1.Tick += Tim1_Tick;
            Tim1.Start();
            InitializeComponent();
            Initial = Mapper.Init();
            MapInit(Mapper);
            Shuffle();
            
        }
        private void Tim1_Tick(object sender, EventArgs e)
        {
            timeSpan += new TimeSpan(0, 0, 0, 0, 100);
            Clock.Content = timeSpan.TotalSeconds.ToString();
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
           // if(CheckWin()) WinnerIndicator.Content = "YOU WIN!";
        }
        private void Shuffle()
        {
            Random rnd = new Random();
            for (var i = 0; i < 1000; i++)
            {
                var t = rnd.Next(4);
                switch (t)
                {
                    case 0:
                        MoveUp();
                        break;
                    case 1:
                        MoveDown();
                        break;
                    case 2:
                        MoveRight();
                        break;
                    case 3:
                        MoveLeft();
                        break;
                }
            }
        }  
        private (int x, int y) GetPositionOfEmpty() => ((Int32)(EmptyRect.GetValue(Grid.RowProperty)),(Int32)(EmptyRect.GetValue(Grid.ColumnProperty)));
        private Button GetUIElementByRC(int posR, int posC)
        {
            UIElement uiElement = null;
            foreach(UIElement element in tgMap.Children)
            {
                if((int)element.GetValue(Grid.RowProperty)==posR && (int)element.GetValue(Grid.ColumnProperty) == posC)
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
            if (ps0.x != 3)
            {
                var BtnToSwap = GetUIElementByRC(ps0.x + 1, ps0.y);
                Move(EmptyRect, BtnToSwap);
            }
        }
        private void MoveDown()
        {
            var ps1 = GetPositionOfEmpty();
            if (ps1.x != 0)
            {
                var BtnToSwap = GetUIElementByRC(ps1.x - 1, ps1.y);
                Move(EmptyRect, BtnToSwap);
            }
        }
        private void MoveLeft()
        {
            var ps2 = GetPositionOfEmpty();
            if (ps2.y != 3)
            {
                var BtnToSwap = GetUIElementByRC(ps2.x, ps2.y + 1);
                Move(EmptyRect, BtnToSwap);
            }
        }
        private void MoveRight()
        {
            var ps3 = GetPositionOfEmpty();
            if (ps3.y != 0)
            {
                var BtnToSwap = GetUIElementByRC(ps3.x, ps3.y - 1);
                Move(EmptyRect, BtnToSwap);
            }
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
