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

namespace Calculator1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResultText.Text = string.Empty; //resetowanie tekstu
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

        private int CalculateResult(string operation)
        {
            if (operation.Contains('+'))
            {
                var elements = operation.Split('+');
                return int.Parse(elements[0]) + int.Parse(elements[1]);
            }

            if (operation.Contains('-'))
            {
                var elements = operation.Split('-');
                return int.Parse(elements[0]) - int.Parse(elements[1]);               
            }

            if (operation.Contains('*'))
            {
                var elements = operation.Split('*');
                return int.Parse(elements[0]) * int.Parse(elements[1]);               
            }

            if (operation.Contains('/'))
            {
                var elements = operation.Split('/');
                return int.Parse(elements[0]) / int.Parse(elements[1]);               
            }
            return default;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {

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

            if(ContainsOperation(operation))
            {
                CurrentOperationText.Text = CalculateResult(operation).ToString();
            }
        }
    }
}