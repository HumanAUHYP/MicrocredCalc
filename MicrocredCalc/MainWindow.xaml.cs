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
        public Dictionary<int, double> daysOnTerm = new Dictionary<int, double>();
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Calculate();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат ввода");
            }
        }

        private void EnterPercents_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnterPercent();
                tbFrom.Text = "";
                tbBy.Text = "";
                tbDetail.Text = "";
                lbPaymentSum.Content = "";
                lbPercentSum.Content = "";
                lbEffRate.Content = "";
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Данные уже записаны, нажмите Reset");
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат ввода");
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            daysOnTerm = null;
            daysOnTerm = new Dictionary<int, double>();
        }

        public void Calculate()
        {
            double loanSum = double.Parse(tbSum.Text);

            double percentSum = 0;
            string detail = "День\tСтавка\tДолг\tСумма выплаты";

            foreach (var pair in daysOnTerm)
            {
                Console.WriteLine($"{pair.Key} {pair.Value}");
                percentSum += loanSum * (pair.Value / 100);
                detail += $"\n{pair.Key}\t{pair.Value}\t{percentSum}\t{loanSum + percentSum}";
            }

            if (percentSum >= loanSum * 1.5)
            {
                MessageBox.Show("Размер выплаты по микрозайму не может превышать 2,5-кратного размера суммы займа");
                return;
            }
            if (loanSum + percentSum >= 500000)
            {
                MessageBox.Show("Предельный размер долговой нагрузки на одно физическое лицо не может превышать 500 тыс. руб");
                return;
            }

            lbPaymentSum.Content = $"Общая сумма выплаты: {Convert.ToString(loanSum + percentSum)}";
            lbPercentSum.Content = $"Сумма процентов: {Convert.ToString(percentSum)}";
            lbEffRate.Content = $"Эффективная ставка: {Convert.ToString(Math.Round(percentSum / loanSum / int.Parse(tbTerm.Text) * 100, 2))}";
            tbDetail.Text = detail;
        }
        public void EnterPercent()
        {
            int term = int.Parse(tbTerm.Text);
            int from = int.Parse(tbFrom.Text);
            int by = int.Parse(tbBy.Text);

            double percent = double.Parse(tbPercent.Text);

            if (percent > 1)
            {
                MessageBox.Show("Максимальная ставка - 1% в день");
                return;
            }

            if (from > term || by > term)
            {
                MessageBox.Show("Вы превысили срок займа");
                return;
            }
            for (int i = from; i <= by; i++)
            {
                daysOnTerm.Add(i, percent);
            }
        }
    }
}
