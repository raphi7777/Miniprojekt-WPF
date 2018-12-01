using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using Condition = ch.hsr.wpf.gadgeothek.domain.Condition;

namespace GadgeothekAdmin
{
    /// <summary>
    /// Interaction logic for CreatNewGadget.xaml
    /// </summary>
    public partial class CreatNewGadget : Window
    {
        public Gadget newGadget { get; set; }
        private LibraryAdminService _adminService;
        private string url = ConfigurationManager.AppSettings["library"];

        public CreatNewGadget()
        {
            InitializeComponent();
            newGadget = null;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _adminService = new LibraryAdminService(url);
            newGadget = new Gadget(TextName.Text);
            newGadget.Manufacturer = TextManufacturer.Text;
            newGadget.Price = System.Convert.ToDouble(TexPrice.Text);
            newGadget.Condition = (Condition)System.Enum.Parse(typeof(Condition), ((ComboBoxItem)ConditionComboBox.SelectedItem).Content.ToString());

            if (_adminService.AddGadget(newGadget))
            {
                Close();
            }
        }

        private void CheckValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
