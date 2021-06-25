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


namespace MicrocredCalc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int term = int.Parse(tbTerm.Text);
                double loanSum = double.Parse(tbSum.Text);

                int firstPart = term / 3;
                int secondPart = firstPart * 2;


                Dictionary<int, double> daysOnTerm = new Dictionary<int, double>();

                for (int i = 1; i < firstPart; i++)
                {
                    daysOnTerm.Add(i, double.Parse(tbFirstPart.Text));
                }
                for (int i = firstPart; i < secondPart; i++)
                {
                    daysOnTerm.Add(i, double.Parse(tbSecondPart.Text));
                }
                for (int i = secondPart; i <= term; i++)
                {
                    daysOnTerm.Add(i, double.Parse(tbThirdPart.Text));
                }

                double percentSum = 0;
                string detail = "День\tСтавка\tДолг\tСумма выплаты";

                foreach (var pair in daysOnTerm)
                {
                    Console.WriteLine($"{pair.Key} {pair.Value}");
                    percentSum += loanSum * (pair.Value / 100);
                    detail += $"\n{pair.Key}\t{pair.Value}\t{percentSum}\t{loanSum + percentSum}";
                }

                lbPaymentSum.Content += Convert.ToString(loanSum + percentSum);
                lbPercentSum.Content += Convert.ToString(percentSum);
                lbEffRate.Content += Convert.ToString(Math.Round(percentSum / loanSum / term * 100, 2));
                tbDetail.Text = detail;
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат ввода");
            }
        }
    }
}
