﻿using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Configuration;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace GadgeothekAdmin
{
    /// <summary>
    /// Interaction logic for CustomerControl.xaml
    /// </summary>
    public partial class CustomerControl : UserControl
    {
        private LibraryAdminService _adminService;
        private string url = ConfigurationManager.AppSettings["library"];
        public ObservableCollection<Customer> ObservableCustomers { get; set; }
        public Customer SelectedCustomer { get; set; }

        public CustomerControl()
        {
            InitializeComponent();
            LoadCustomers();
            DataContext = this;
        }

        private void CustomerCreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewCustomer createCustomer = new CreateNewCustomer();
            createCustomer.ShowDialog();

            if (!createCustomer.newCustomer.Name.Equals(""))
            {
                ObservableCustomers.Add(createCustomer.newCustomer);
            }
        }

        private void CustomerDeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            string deleteMessageText = $"Are you sure you want delete the selected Customer?";

            if (MessageBox.Show(deleteMessageText, "Delete customer", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _adminService.DeleteCustomer(SelectedCustomer);
                ObservableCustomers.Remove(SelectedCustomer);
            }
        }

        private void LoadCustomers()
        {
            _adminService = new LibraryAdminService(url);
            ObservableCustomers = new ObservableCollection<Customer>();
            foreach (Customer customer in _adminService.GetAllCustomers())
            {
                ObservableCustomers.Add(customer);
            }
        }

        private void CustomerEditButton_OnClick(object sender, RoutedEventArgs e)
        {
            CreateNewCustomer editCustomer = new CreateNewCustomer(SelectedCustomer);
            editCustomer.ShowDialog();
        }
    }
}
