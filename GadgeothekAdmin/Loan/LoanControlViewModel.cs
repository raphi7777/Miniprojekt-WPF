using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ch.hsr.wpf.gadgeothek.domain;

namespace GadgeothekAdmin
{
    public class LoanControlViewModel : BindableBase
    {
        public LoanControlViewModel(ObservableCollection<Loan> observableLoans)
        {
            ObservableLoans = observableLoans;
        }
        public ObservableCollection<Loan> ObservableLoans { get; set; }
        private Loan _selectedLoan;
        public Loan SelectedLoan
        {
            get { return _selectedLoan; }
            set
            {
                _selectedLoan = value;
                _selectedLoanReturnDate = _selectedLoan.ReturnDate;
            }
        }
        private DateTime? _selectedLoanReturnDate;
        public DateTime? SelectedLoanReturnDate
        {
            get { return _selectedLoanReturnDate; }
            set { SetProperty(ref _selectedLoanReturnDate, value); }
        }
    }
}
