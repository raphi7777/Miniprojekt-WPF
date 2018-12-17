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
    public partial class EditCustomer : Window
    {
        public Customer editCustomer { get; set; }
        private LibraryAdminService _adminService;
        private string url = ConfigurationManager.AppSettings["library"];

        public EditCustomer(Customer customer)
        {
            InitializeComponent();
            editCustomer = customer;
            DataContext = editCustomer;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _adminService = new LibraryAdminService(url);

            if(_adminService.UpdateCustomer(editCustomer))
            {
                Close();
            }
        }
    }
}
