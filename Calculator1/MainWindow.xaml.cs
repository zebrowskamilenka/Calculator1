using System.Text;
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

namespace Calculator1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            ResultText.Text = string.Empty; //resetowanie tekstu
            this.Focusable = true;
            this.Focus(); //nasłuch klawiatury
            StartClock();
        }

        private void Window_TextInput(object sender, TextCompositionEventArgs e)
        {
            string input = e.Text;
            if ("0123456789".Contains(input))
            {
                ResultText.Text = string.Empty;
                var currentNumber = input;
                CurrentOperationText.Text += currentNumber;

            }
            if ("+-*/".Contains(input))
            {
                var operation = CurrentOperationText.Text;
                CurrentOperationText.Text += input;
                if (ContainsOperation(operation))
                {
                    CurrentOperationText.Text = CalculateResult(operation).ToString();
                }
            }
            if ("cC".Contains(input))
            {
                CurrentOperationText.Text = string.Empty;
                ResultText.Text = string.Empty;
            }
            if ("=".Contains(input))
            {
                Equals_Click(this, new RoutedEventArgs());
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            ResultText.Text = string.Empty; //resetowanie tekstu
            var button = sender as Button;
            var currentNumber = button.Name[button.Name.Length - 1];
            CurrentOperationText.Text += currentNumber;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            var operation = CurrentOperationText.Text;


            ResultText.Text = CalculateResult(operation).ToString();

            CurrentOperationText.Text = string.Empty;
        }

        private bool ContainsOperation(string operation)
        {
            return operation.Contains('+') || operation.Contains('-') || operation.Contains('*') || operation.Contains('/');
        }

        private float CalculateResult(string operation)
        {
            if (operation.Contains('+'))
            {
                var elements = operation.Split('+');
                return float.Parse(elements[0]) + float.Parse(elements[1]);
            }

            if (operation.Contains('-'))
            {
                var elements = operation.Split('-');
                return float.Parse(elements[0]) - float.Parse(elements[1]);
            }

            if (operation.Contains('*'))
            {
                var elements = operation.Split('*');
                return float.Parse(elements[0]) * float.Parse(elements[1]);
            }

            if (operation.Contains('/'))
            {
                var elements = operation.Split('/');
                return float.Parse(elements[0]) / float.Parse(elements[1]);
            }
            return default;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            CurrentOperationText.Text = string.Empty;
            ResultText.Text = string.Empty;
        }

        private void Operation_ClickDivide(object sender, RoutedEventArgs e)
        {
            var operation = CurrentOperationText.Text;
            CurrentOperationText.Text += "/";

            if (ContainsOperation(operation))
            {
                CurrentOperationText.Text = CalculateResult(operation).ToString();
            }
        }

        private void Operation_ClickMultiply(object sender, RoutedEventArgs e)
        {

            var operation = CurrentOperationText.Text;
            CurrentOperationText.Text += "*";

            if (ContainsOperation(operation))
            {
                CurrentOperationText.Text = CalculateResult(operation).ToString();
            }
        }

        private void Operation_ClickMinus(object sender, RoutedEventArgs e)
        {

            var operation = CurrentOperationText.Text;
            CurrentOperationText.Text += "-";

            if (ContainsOperation(operation))
            {
                CurrentOperationText.Text = CalculateResult(operation).ToString();
            }
        }

        private void Operation_ClickPlus(object sender, RoutedEventArgs e)
        {

            var operation = CurrentOperationText.Text;
            CurrentOperationText.Text += "+";

            if (ContainsOperation(operation))
            {
                CurrentOperationText.Text = CalculateResult(operation).ToString();
            }
        }
        private void StartClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private bool isAnalog = false;
        private void ToggleClockButton_Click(object sender, RoutedEventArgs e)
        {
            isAnalog = !isAnalog;
            if (isAnalog)
            {
                AnalogClock.Visibility = Visibility.Visible;
                Clock.Visibility = Visibility.Hidden;
            }
            else
            {
                AnalogClock.Visibility = Visibility.Hidden;
                Clock.Visibility = Visibility.Visible;
            }
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            Clock.Text = DateTime.Now.ToString("HH:mm:ss");
            UpdateAnalogClock(DateTime.Now);
        }
        private void UpdateAnalogClock(DateTime now)
        {
            AnalogClock.Children.Clear();

            double centerX = AnalogClock.Width / 2;
            double centerY = AnalogClock.Height / 2;

            DrawHand(centerX, centerY, now.Hour % 12 * 30 + now.Minute * 0.5, 30, 4);
            DrawHand(centerX, centerY, now.Minute * 6, 45, 3);
            DrawHand(centerX, centerY, now.Second * 6, 55, 2);
        }
        private void DrawHand(double cx, double cy, double angleDeg, double length, double thickness)
        {
            double angleRad = (Math.PI / 180) * (angleDeg - 90);

            double x = cx + length * Math.Cos(angleRad);
            double y = cy + length * Math.Sin(angleRad);

            var line = new Line
            {
                X1 = cx,
                Y1 = cy,
                X2 = x,
                Y2 = y,
                Stroke = Brushes.Black,
                StrokeThickness = thickness
            };

            AnalogClock.Children.Add(line);
        }
        private bool isDarkMode = false;
        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            isDarkMode = !isDarkMode;
            if (isDarkMode)
            {
                this.Style = (Style)FindResource("DarkStyle");
            }
            else
            {
                this.Style = (Style)FindResource("LightStyle");
            }
        }
    }
}