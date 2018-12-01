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

namespace GadgeothekAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadSelectedControl(new GadgetControl());
        }

        private void ButtonGadgetList_Checked(object sender, RoutedEventArgs e)
        {
            ButtonGadgetList.IsChecked = true;
            ButtonLoanList.IsChecked = false;
            ButtonCustomerList.IsChecked = false;

            LoadSelectedControl(new GadgetControl());
        }

        private void ButtonLoanList_Checked(object sender, RoutedEventArgs e)
        {
            ButtonLoanList.IsChecked = true;
            ButtonGadgetList.IsChecked = false;
            ButtonCustomerList.IsChecked = false;

            LoadSelectedControl(new LoanControl());

        }

        private void ButtonCustomerList_Checked(object sender, RoutedEventArgs e)
        {
            ButtonCustomerList.IsChecked = true;
            ButtonLoanList.IsChecked = false;
            ButtonGadgetList.IsChecked = false;

            LoadSelectedControl(new CustomerControl());
        }

        private void LoadSelectedControl(UserControl control)
        {
            ListControlPanel.Children.Clear();
            ListControlPanel.Children.Add(control);
        }
    }
}
