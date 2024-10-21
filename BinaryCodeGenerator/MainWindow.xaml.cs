using System;
using System.Windows;
using System.Linq;
using System.IO.Ports;
using System.Net.Sockets;

namespace BinaryErrorDetection
{
    public partial class MainWindow : Window
    {
        private const int N = 6;  // кол-во разрядов в коде
        private const int K = 8;  // кол-во кодов
        private Random random = new Random();
        private int[][] codes = new int[K][];
        private int[] parities = new int[K];
        private int[] controlCode = new int[N];  // контрольная кодовая комбинация

        public MainWindow()
        {
            InitializeComponent();
        }

        // генерация случайных кодов
        private void GenerateCodes(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < K; i++)
            {
                codes[i] = new int[N];
                for (int j = 0; j < N; j++)
                {
                    codes[i][j] = random.Next(2);  // генерация 0 или 1
                }
            }
            UpdateCodeDisplays();
        }

        // обновление интерфейса для отображения кодов
        private void UpdateCodeDisplays()
        {
            Code1.Text = string.Join("", codes[0]);
            Code2.Text = string.Join("", codes[1]);
            Code3.Text = string.Join("", codes[2]);
            Code4.Text = string.Join("", codes[3]);
            Code5.Text = string.Join("", codes[4]);
            Code6.Text = string.Join("", codes[5]);
            Code7.Text = string.Join("", codes[6]);
            Code8.Text = string.Join("", codes[7]);
        }

        // генерация контрольных разрядов на нечетность
        private void GenerateParities(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < K; i++)
            {
                parities[i] = codes[i].Sum() % 2 == 0 ? 1 : 0;  // нечётность
            }
            UpdateParityDisplays();
            GenerateControlCode();  // генерация контрольной комбинации
        }

        // обновление интерфейса для отображения контрольных разрядов
        private void UpdateParityDisplays()
        {
            Parity1.Text = parities[0].ToString();
            Parity2.Text = parities[1].ToString();
            Parity3.Text = parities[2].ToString();
            Parity4.Text = parities[3].ToString();
            Parity5.Text = parities[4].ToString();
            Parity6.Text = parities[5].ToString();
            Parity7.Text = parities[6].ToString();
            Parity8.Text = parities[7].ToString();
        }

        // генерация контрольной кодовой комбинации
        private void GenerateControlCode()
        {
            for (int j = 0; j < N; j++)
            {
                int sum = 0;
                for (int i = 0; i < K; i++)
                {
                    sum += codes[i][j];  // суммирование по столбцам
                }
                controlCode[j] = sum % 2 == 0 ? 1 : 0;  // нечётность
            }
            ControlCode.Text = string.Join("", controlCode);  // обновление поля контрольной комбинации
        }

        // выход из программы
        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
