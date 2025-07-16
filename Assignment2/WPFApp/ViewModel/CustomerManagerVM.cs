using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BusinessObject;
using DataAccessLayer;
using WPFApp.Popup;


namespace WPFApp.ViewModel
{
    public class CustomerManagerViewModel : INotifyPropertyChanged
    {
        private readonly CustomerDAO _dao = new CustomerDAO();
        public List<int> StatusOptions { get; } = new() { 1, 2 }; // 1: Active, 2: Deleted
        private int _customerStatus;
        public int CustomerStatus
        {
            get => _customerStatus;
            set
            {
                _customerStatus = value;
                OnPropertyChanged(nameof(CustomerStatus));
            }
        }

        public ObservableCollection<Customer> Customers { get; set; }
        public ICollectionView FilteredCustomers { get; }

        private string _keyword = "";
        public string Keyword
        {
            get => _keyword;
            set { _keyword = value; OnPropertyChanged(); FilteredCustomers.Refresh(); }
        }

        private Customer? _selectedCustomer;
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(); }
        }

        public CustomerManagerViewModel()
        {
            Customers = new ObservableCollection<Customer>(_dao.GetAllCustomers());
            FilteredCustomers = CollectionViewSource.GetDefaultView(Customers);
            FilteredCustomers.Filter = _ => FilterByKeyword(_);
        }

        private bool FilterByKeyword(object item)
        {
            if (item is not Customer c) return false;
            return string.IsNullOrWhiteSpace(Keyword) ||
                   c.CustomerFullName.Contains(Keyword, StringComparison.OrdinalIgnoreCase) ||
                   c.EmailAddress.Contains(Keyword, StringComparison.OrdinalIgnoreCase) ||
                   c.Telephone.Contains(Keyword, StringComparison.OrdinalIgnoreCase);
        }

        // ───── Commands ─────
        public ICommand CreateCommand => new RelayCmd(_ => OpenDialogForCreate());
        public ICommand EditCommand => new RelayCmd(_ => OpenDialogForEdit(), _ => SelectedCustomer != null);
        public ICommand DeleteCommand => new RelayCmd(_ => DeleteSelected(), _ => SelectedCustomer != null);

        private void OpenDialogForCreate()
        {
            var fresh = new Customer { CustomerStatus = 1, CustomerBirthday = DateOnly.FromDateTime(DateTime.Now) };
            var dlg = new CustomerDialog(fresh) { Title = "Create Customer" };
            if (dlg.ShowDialog() == true)
            {
                _dao.AddCustomer(fresh);
                Customers.Add(fresh);
            }
        }

        private void OpenDialogForEdit()
        {
            // clone to avoid editing original if user cancels
            var clone = new Customer
            {
                CustomerId = SelectedCustomer!.CustomerId,
                CustomerFullName = SelectedCustomer.CustomerFullName,
                Telephone = SelectedCustomer.Telephone,
                EmailAddress = SelectedCustomer.EmailAddress,
                CustomerBirthday = SelectedCustomer.CustomerBirthday,
                CustomerStatus = SelectedCustomer.CustomerStatus,
                Password = SelectedCustomer.Password
            };

            var dlg = new CustomerDialog(clone) { Title = "Update Customer" };
            if (dlg.ShowDialog() == true)
            {
                _dao.UpdateCustomer(clone);

                // copy back into original object so grid refreshes
                SelectedCustomer.CustomerFullName = clone.CustomerFullName;
                SelectedCustomer.Telephone = clone.Telephone;
                SelectedCustomer.EmailAddress = clone.EmailAddress;
                SelectedCustomer.CustomerBirthday = clone.CustomerBirthday;
                SelectedCustomer.CustomerStatus = clone.CustomerStatus;
                SelectedCustomer.Password = clone.Password;
                FilteredCustomers.Refresh();
            }
        }

        private void DeleteSelected()
        {
            if (MessageBox.Show("Are you sure you want to delete this customer?",
                                "Confirm delete", MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _dao.DeleteCustomer(SelectedCustomer!.CustomerId);
                Customers.Remove(SelectedCustomer);
            }
        }

        // ───── INotifyPropertyChanged ─────
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? p = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }

}