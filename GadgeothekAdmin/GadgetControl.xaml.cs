using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace GadgeothekAdmin
{
    /// <summary>
    /// Interaction logic for GadgetControl.xaml
    /// </summary>
    public partial class GadgetControl : UserControl
    {
        private LibraryAdminService _adminService;
        private string url = ConfigurationManager.AppSettings["library"];

        public ObservableCollection<Gadget> ObservableGadgets { get; set; }
        public Gadget SelectedGadget { get; set; }

        public GadgetControl()
        {
            InitializeComponent();
            LoadGadgets();
            DataContext = this;
        }

        private void GadgetCreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreatNewGadget createGadget = new CreatNewGadget();
            createGadget.ShowDialog();

            if (createGadget.newGadget != null)
            {
                ObservableGadgets.Add(createGadget.newGadget);
            }
        }

        private void GadgetDeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            string deleteMessageText = $"Are you sure you want delete the selected Gadget?";

            if (MessageBox.Show(deleteMessageText, "Delete gadget", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _adminService.DeleteGadget(SelectedGadget);
                ObservableGadgets.Remove(SelectedGadget);
            }
        }

        private void LoadGadgets()
        {
            _adminService = new LibraryAdminService(url);
            ObservableGadgets = new ObservableCollection<Gadget>();
            foreach (Gadget gadget in _adminService.GetAllGadgets())
            {
                ObservableGadgets.Add(gadget);
            }
        }

    }
}
