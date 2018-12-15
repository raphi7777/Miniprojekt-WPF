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
        public Customer editCustomer { get; set; }
        private LibraryAdminService _adminService;
        private string url = ConfigurationManager.AppSettings["library"];

        public CreateNewCustomer()
        {
            InitializeComponent();
            newCustomer = new Customer();
            DataContext = newCustomer;
        }

        public CreateNewCustomer(Customer customer)
        {
            InitializeComponent();
            editCustomer = customer;
            DataContext = editCustomer;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (newCustomer != null && newCustomer.Studentnumber != null)
            {
                _adminService = new LibraryAdminService(url);

                if (editCustomer != null)
                {
                    if (_adminService.UpdateCustomer(editCustomer))
                    {
                        Close();
                    }
                }
                else
                {
                    if (_adminService.AddCustomer(newCustomer))
                    {
                        Close();
                    }
                }
            }
            else
            {
                //TODO dialog warning
            }
        }

        private void CheckValidation(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
