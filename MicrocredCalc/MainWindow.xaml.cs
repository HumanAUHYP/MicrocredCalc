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
            int term = int.Parse(tbTerm.Text);
            int loanSum = int.Parse(tbSum.Text);

            int firstPart = term / 3;
            int secondPart = firstPart * 2;

            Dictionary<int, float> daysOnTerm = new Dictionary<int, float>();

            for (int i = 1; i < firstPart; i++)
            {
                daysOnTerm.Add(i, float.Parse(tbFirstPart.Text));
            }
            for (int i = firstPart; i < secondPart; i++)
            {
                daysOnTerm.Add(i, float.Parse(tbSecondPart.Text));
            }
            for (int i = secondPart; i <= term; i++)
            {
                daysOnTerm.Add(i, float.Parse(tbThirdPart.Text));
            }

            foreach (var pair in daysOnTerm)
            {
                Console.WriteLine("{0} {1}", pair.Key, pair.Value);

            }

            
        }
    }
}
