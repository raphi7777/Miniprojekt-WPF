using System.Windows;
using System.Windows.Controls;

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
