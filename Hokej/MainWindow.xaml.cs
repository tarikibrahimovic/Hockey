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

namespace Hokej
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        double x, y, x1, y1, x2, y2;
        int tPak, iPak;
        bool igrac1G = false, igrac1D = false;
        bool igrac2G, igrac2D;
        int rezultat1, rezultat2;
        bool udaracPak;
        int brzinaIgre;

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                igrac1G = false;
            }
            if (e.Key == Key.S)
            {
                igrac1D = false;
            }
            if (e.Key == Key.I)
            {
                igrac2G = false;
            }
            if (e.Key == Key.K)
            {
                igrac2D = false;
            }
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                igrac1G = true;
            }
            if (e.Key == Key.S)
            {
                igrac1D = true;
            }
            if (e.Key == Key.K)
            {
                igrac2D = true;
            }
            if (e.Key == Key.I)
            {
                igrac2G = true;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            glavni.Focus();
            brzinaIgre = 5;
            tPak = 0;
            iPak = 1;
            rezultat1 = 0;
            rezultat2 = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(40);
            timer.Tick += Timer_Tick;
        }

        public void UzmiKoordinate()
        {
            x1 = Canvas.GetLeft(recigrac1);
            y1 = Canvas.GetTop(recigrac1);
            x2 = Canvas.GetLeft(recigrac2);
            y2 = Canvas.GetTop(recigrac2);
            x = Canvas.GetLeft(epak);
            y = Canvas.GetTop(epak);
        }

        private void bkreni_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            glavni.Focus();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UzmiKoordinate();
            PokretIgraca();
            Hit();
            HitBorder();
            if (udaracPak == true)
            {
                Canvas.SetLeft(epak, 270);
                Canvas.SetTop(epak, 182);
                udaracPak = false;
            }
            else
            {
                if (tPak == 0)
                {
                    Canvas.SetLeft(epak, x + brzinaIgre);
                }
                else
                {
                    Canvas.SetLeft(epak, x - brzinaIgre);
                }
                if (iPak == 1)
                {
                    Canvas.SetTop(epak, y + brzinaIgre);
                }
                else if (iPak == 2)
                {
                    Canvas.SetTop(epak, y - brzinaIgre);
                }
            }
        }

        public void Hit()
        {
            Rect igrac1 = new Rect(x1, y1, recigrac1.Width, recigrac1.Height);
            Rect igrac2 = new Rect(x2, y2, recigrac2.Width, recigrac2.Height);
            Rect pak = new Rect(x, y, epak.Width, epak.Height);
            if (x < x1 - 5)
            {
                rezultat1++;
                rez1.Text = rezultat1.ToString();
                udaracPak = true;

            }
            else if (x == x2 + 5)
            {
                rezultat2++;
                rez2.Text = rezultat2.ToString();
                udaracPak = true;
            }
            if (igrac1.IntersectsWith(pak))
            {
                tPak = 0;
            }
            else if (igrac2.IntersectsWith(pak))
            {
                tPak = 1;
            }
        }

        public void HitBorder()
        {
            if (y < 5)
            {
                iPak = 1;
            }
            else if (y > 344)
            {
                iPak = 2;
            }
        }
        public void PokretIgraca()
        {
            if (y1 > 3)
            {
                if (igrac1G == true)
                {
                    Canvas.SetTop(recigrac1, y1 - brzinaIgre);
                }
            }
            if (y1 < 319)
            {
                if (igrac1D == true)
                {
                    Canvas.SetTop(recigrac1, y1 + brzinaIgre);
                }
            }
            if (y2 > 3)
            {
                if (igrac2G == true)
                {
                    Canvas.SetTop(recigrac2, y2 - brzinaIgre);
                }
            }
            if (y2 < 319)
            {
                if (igrac2D == true)
                {
                    Canvas.SetTop(recigrac2, y2 + brzinaIgre);
                }
            }
        }


        private void bprekini_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Canvas.SetLeft(epak, 270);
            Canvas.SetTop(epak, 182);
            Canvas.SetLeft(recigrac1, 10);
            Canvas.SetTop(recigrac1, 170);
            Canvas.SetTop(recigrac2, 170);
            Canvas.SetLeft(recigrac2, 535);
            rezultat1 = 0;
            rezultat2 = 0;
            rez1.Text = 0.ToString();
            rez2.Text = 0.ToString();
            glavni.Focus();
        }

        private void bstani_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            glavni.Focus();
        }
    }
}
