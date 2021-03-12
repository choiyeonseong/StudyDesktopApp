using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeBiteGame
{
    /// <summary>
    /// GameWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GameWindow : Window
    {
        Random rand = new Random();

        Ellipse[] snake = new Ellipse[30];
        Ellipse egg = new Ellipse();

        private int size = 12;  // egg, body
        private int visibleCount = 5;   // 처음 나오는 뱀 길이
        private string move = "";   // 뱀 이동 방향
        private int eaten = 0;  // 먹은 알의 개수(25가 되면 성공)

        DispatcherTimer playTimer = new DispatcherTimer();
        Stopwatch stopwatch = new Stopwatch();

        private bool startFlag = false;

        public GameWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Initialized(object sender, EventArgs e)
        {
            InitSnake();
            InitEgg();

            playTimer.Interval = new TimeSpan(0, 0, 0, 0, 100); // 0.1초
            playTimer.Tick += PlayTimer_Tick;
            playTimer.Start();
        }

        private void InitSnake()
        {
            // 뱀이 생성될 위치 - 랜덤
            int x = rand.Next(1, 500 / size) * size;
            int y = rand.Next(1, 400 / size) * size;

            // 뱀 모양 초기화
            for (int i = 0; i < 30; i++)
            {
                snake[i] = new Ellipse();
                snake[i].Width = snake[i].Height = size;
                if (i == 0)
                    snake[i].Fill = Brushes.Chocolate;  // 머리
                else if (i % 5 == 0)
                    snake[i].Fill = Brushes.YellowGreen;    // 5번째 마디
                else
                    snake[i].Fill = Brushes.Gold;
                snake[i].Stroke = Brushes.Black;

                CvsGame.Children.Add(snake[i]);
            }

            // 뱀 길이를 5개(초기 visibleCount = 5)로 하고 나머지는 화면에서 지움
            for (int i = visibleCount; i < 30; i++)
            {
                snake[i].Visibility = Visibility.Hidden;
            }

            CreateSnake(x, y);
        }

        private void CreateSnake(int x, int y)
        {
            for (int i = 0; i < visibleCount; i++)
            {
                snake[i].Tag = new Point(x, y + i * size);
                Canvas.SetLeft(snake[i], x);
                Canvas.SetTop(snake[i], y + i * size);
            }
        }

        private void InitEgg()
        {
            
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            // 처음 시작 (move == 0)
            if (move !="")
            {
                startFlag = true;

                for (int i = visibleCount; i >= 1 ; i--)    // 꼬리 하나를 더 계산
                {
                    Point p = (Point)snake[i - 1].Tag;
                    snake[i].Tag = new Point(p.X, p.Y);
                }

                // TODO : 키보드로 옮겼을때 처리

                // TODO : 알을 먹었는지 체크
            }

            // 스탑워치
            if (startFlag)
            {
                TimeSpan span = stopwatch.Elapsed;
                TxtTime.Text = $"Time = {span.Minutes}:{span.Seconds}:{span.Milliseconds / 10}";
                DrawSnake();
            }
        }

        private void DrawSnake()
        {
            for (int i = 0; i < visibleCount; i++)
            {
                Point p = (Point)snake[i].Tag;
                Canvas.SetLeft(snake[i], p.X);
                Canvas.SetTop(snake[i], p.Y);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // 처음 시작하면 스탑워치 시작
            if (move=="") stopwatch.Start();
            
            // TODO : 키조작
        }

    }
}
