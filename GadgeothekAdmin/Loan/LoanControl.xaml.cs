using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace GadgeothekAdmin
{
    /// <summary>
    /// Interaction logic for LoanControl.xaml
    /// </summary>
    public partial class LoanControl : UserControl
    {
        private LibraryAdminService _adminService;
        private string url = ConfigurationManager.AppSettings["library"];
        private LoanControlViewModel Model { get; set; }

        public LoanControl()
        {
            InitializeComponent();
            _adminService = new LibraryAdminService(url);
            Model = new LoanControlViewModel(LoadLoans());
            DataContext = Model;
            System.Windows.Forms.Timer refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 10000;
            refreshTimer.Enabled = true;
            refreshTimer.Start();
            refreshTimer.Tick += new EventHandler(TimerEvent);

        }

        private void LoanCreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewLoan createLoan = new CreateNewLoan();
            createLoan.ShowDialog();
        }

        private void LoanEndButton_OnClick(object sender, RoutedEventArgs e)
        {
            string endMessageText = $"Are you sure you want to end the selected Loan?";

            if (MessageBox.Show(endMessageText, "End loan", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Model.SelectedLoan.ReturnDate = DateTime.Now;
                _adminService.UpdateLoan(Model.SelectedLoan);
            }
        }

        private ObservableCollection<Loan> LoadLoans()
        {
            ObservableCollection<Loan> observableLoans = new ObservableCollection<Loan>();
            foreach (Loan loan in _adminService.GetAllLoans())
            {
                observableLoans.Add(loan);
            }
            ObservableCollection<Loan> observableLoansSorted = new ObservableCollection<Loan>(observableLoans.OrderByDescending(loan => loan.PickupDate));
            return observableLoansSorted;
        }


        private void TimerEvent(Object myObject, EventArgs myEventArgs)
        {
            Model = new LoanControlViewModel(LoadLoans());
            DataContext = Model;
        }
    }
}
