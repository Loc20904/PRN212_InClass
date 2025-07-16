using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BusinessObject;
using System.Xml.Linq;
using DataAccessLayer;
using WPFApp.Popup;

namespace WPFApp.ViewModel
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class IntToNullableByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is int intValue ? (byte?)intValue : value;
    }

    public class RoomManagerViewModel : INotifyPropertyChanged
    {
        private readonly RoomInformationDAO _dao = new RoomInformationDAO();
        

        public List<byte> StatusOptions { get; } = new() { 1, 2 }; // 1: Active, 2: Deleted
        private int _roomStatus;
        public int RoomStatus
        {
            get => _roomStatus;
            set
            {
                _roomStatus = value;
                OnPropertyChanged(nameof(RoomStatus));
            }
        }

        public ObservableCollection<RoomInformation> Rooms { get; set; }
        public ICollectionView FilteredRooms { get; }

        private string _keyword = "";
        public string Keyword
        {
            get => _keyword;
            set { _keyword = value; OnPropertyChanged(); FilteredRooms.Refresh(); }
        }

        private RoomInformation? _selectedRoom;
        public RoomInformation? SelectedRoom
        {
            get => _selectedRoom;
            set { _selectedRoom = value; OnPropertyChanged(); }
        }

        public RoomManagerViewModel()
        {
            Rooms = new ObservableCollection<RoomInformation>(_dao.GetAll());
            FilteredRooms = CollectionViewSource.GetDefaultView(Rooms);
            FilteredRooms.Filter = _ => FilterByKeyword(_);
        }

        private bool FilterByKeyword(object item)
        {
            if (item is not RoomInformation r) return false;
            return string.IsNullOrWhiteSpace(Keyword) ||
                   r.RoomNumber.Contains(Keyword, StringComparison.OrdinalIgnoreCase) ||
                   r.RoomDetailDescription.Contains(Keyword, StringComparison.OrdinalIgnoreCase);
        }

        // ───── Commands ─────
        public ICommand CreateCommand => new RelayCmd(_ => OpenDialogForCreate());
        public ICommand EditCommand => new RelayCmd(_ => OpenDialogForEdit(), _ => SelectedRoom != null);
        public ICommand DeleteCommand => new RelayCmd(_ => DeleteSelected(), _ => SelectedRoom != null);

        private void OpenDialogForCreate()
        {
            var fresh = new RoomInformation { RoomStatus = 1, RoomTypeId = 1 };
            var dlg = new RoomDialog(fresh) { Title = "Create Room" };
            if (dlg.ShowDialog() == true)
            {
                _dao.Add(fresh);
                Rooms.Add(fresh);
            }
        }

        private void OpenDialogForEdit()
        {
            // clone to avoid editing original if user cancels
            var clone = new RoomInformation
            {
                RoomId = SelectedRoom!.RoomId,
                RoomNumber = SelectedRoom.RoomNumber,
                RoomDetailDescription = SelectedRoom.RoomDetailDescription,
                RoomMaxCapacity = SelectedRoom.RoomMaxCapacity,
                RoomStatus = SelectedRoom.RoomStatus,
                RoomPricePerDay = SelectedRoom.RoomPricePerDay,
                RoomTypeId = SelectedRoom.RoomTypeId
            };

            var dlg = new RoomDialog(clone) { Title = "Update Room" };
            if (dlg.ShowDialog() == true)
            {
                _dao.Update(clone);

                // copy back into original object so grid refreshes
                SelectedRoom.RoomNumber = clone.RoomNumber;
                SelectedRoom.RoomDetailDescription = clone.RoomDetailDescription;
                SelectedRoom.RoomMaxCapacity = clone.RoomMaxCapacity;
                SelectedRoom.RoomStatus = clone.RoomStatus;
                SelectedRoom.RoomPricePerDay = clone.RoomPricePerDay;
                SelectedRoom.RoomTypeId = clone.RoomTypeId;
                FilteredRooms.Refresh();
            }
        }

        private void DeleteSelected()
        {
            if (MessageBox.Show("Are you sure you want to delete this room?",
                                "Confirm delete", MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                //check if room is have in reservation then change status to 0
                if(new BookingReservationDAO().GetAll().Any(bk=>bk.BookingDetails.Any(b=>b.RoomId == SelectedRoom.RoomId)))
                {
                    _dao.Delete(SelectedRoom!.RoomId, false);
                    SelectedRoom.RoomStatus = (byte)2;
                }
                else
                {
                    _dao.Delete(SelectedRoom!.RoomId, true);
                    Rooms.Remove(SelectedRoom);
                }
                FilteredRooms.Refresh();
            }
        }

        // ───── INotifyPropertyChanged ─────
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? p = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }

    /// very small helper ICommand
    public record RelayCmd(Action<object?> Executes, Predicate<object?>? Can = null) : ICommand
    {
        public bool CanExecute(object? p) => Can?.Invoke(p) ?? true;
        public void Execute(object? p) => Executes(p);
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }



}
