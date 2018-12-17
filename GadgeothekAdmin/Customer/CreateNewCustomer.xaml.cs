using System.Windows;
using System.Windows.Input;
using System.Configuration;
using System.Text.RegularExpressions;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace GadgeothekAdmin
{
    /// <summary>
    /// Interaction logic for CreateNewCustomer.xaml
    /// </summary>
    public partial class CreateNewCustomer : Window
    {
        public Customer newCustomer { get; set; }
        private LibraryAdminService _adminService;
        private string url = ConfigurationManager.AppSettings["library"];

        public CreateNewCustomer()
        {
            InitializeComponent();
            newCustomer = new Customer();
            DataContext = newCustomer;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (newCustomer.Studentnumber != null)
            {
                _adminService = new LibraryAdminService(url);

                if (_adminService.AddCustomer(newCustomer))
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("You have not filled in all the values yet.", "Value missing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CheckValidation(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
