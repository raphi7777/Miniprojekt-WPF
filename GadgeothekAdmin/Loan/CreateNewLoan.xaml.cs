using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace GadgeothekAdmin
{
    public partial class CreateNewLoan : Window
    {
        public Loan NewLoan { get; set; }
        private LibraryAdminService _adminService;
        private string url = ConfigurationManager.AppSettings["library"];
        public ObservableCollection<Customer> ObservableCustomers { get; set; }
        public ObservableCollection<Gadget> ObservableGadgets { get; set; }
        public string Id
        {
            get { return NewLoan.Id; }
            set { NewLoan.Id = value; }
        }
        public Customer Customer
        {
            get { return NewLoan.Customer; }
            set { NewLoan.Customer = value; }
        }
        public Gadget Gadget
        {
            get { return NewLoan.Gadget; }
            set { NewLoan.Gadget = value; }
        }
        public DateTime? PickupDate
        {
            get { return NewLoan.PickupDate; }
            set { NewLoan.PickupDate = value; }
        }
        public CreateNewLoan()
        {
            InitializeComponent();
            _adminService = new LibraryAdminService(url);
            NewLoan = new Loan();
            LoadCustomers();
            LoadGadgets();
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewLoan.Id != null && NewLoan.Customer != null && NewLoan.Gadget != null && NewLoan.PickupDate != null && _adminService.AddLoan(NewLoan))
            {
                Close();
            } 
            else
            {
                MessageBox.Show("You have not filled in all the values yet.", "Value missing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadCustomers()
        {
            ObservableCustomers = new ObservableCollection<Customer>();
            foreach (Customer customer in _adminService.GetAllCustomers())
            {
                ObservableCustomers.Add(customer);
            }
        }
        private void LoadGadgets()
        {
            ObservableGadgets = new ObservableCollection<Gadget>();
            foreach (Gadget gadget in _adminService.GetAllGadgets())
            {
                ObservableGadgets.Add(gadget);
            }
        }
    }
}
