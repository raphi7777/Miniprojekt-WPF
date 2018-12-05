using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace GadgeothekAdmin
{
    public partial class CreatNewGadget : Window
    {
        public Gadget newGadget { get; set; }
        private LibraryAdminService _adminService;
        private string url = ConfigurationManager.AppSettings["library"];

        public CreatNewGadget()
        {
            InitializeComponent();
            newGadget = new Gadget("");
            DataContext = newGadget;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _adminService = new LibraryAdminService(url);

            if (_adminService.AddGadget(newGadget))
            {
                Close();
            }
        }

        private void CheckValidation(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
