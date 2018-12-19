using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace GadgeothekAdmin
{
    public partial class GadgetControl : UserControl
    {
        private LibraryAdminService _adminService;
        private string url = ConfigurationManager.AppSettings["library"];
        public ObservableCollection<Gadget> ObservableGadgets { get; set; }
        public Gadget SelectedGadget { get; set; }

        public GadgetControl()
        {
            InitializeComponent();
            ObservableGadgets = new ObservableCollection<Gadget>();
            LoadGadgets();
            DataContext = this;
        }

        private void GadgetCreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewGadget createGadget = new CreateNewGadget();
            createGadget.ShowDialog();
            ObservableGadgets.Clear();
            LoadGadgets();
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
            foreach (Gadget gadget in _adminService.GetAllGadgets())
            {
                ObservableGadgets.Add(gadget);
            }
        }

        private void GadgetEditButton_OnClick(object sender, RoutedEventArgs e)
        {
            CreateNewGadget editGadget = new CreateNewGadget(SelectedGadget);
            editGadget.ShowDialog();
            ObservableGadgets.Clear();
            LoadGadgets();
        }
    }
}
