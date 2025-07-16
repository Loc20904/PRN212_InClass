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
    public class RoomTypeManagerViewModel : INotifyPropertyChanged
    {
        private readonly RoomTypeDAO _dao = new RoomTypeDAO();
        public ObservableCollection<RoomType> RoomTypes { get; set; }
        public ICollectionView FilteredRoomTypes { get; }

        private string _keyword = "";
        public string Keyword
        {
            get => _keyword;
            set
            {
                _keyword = value;
                OnPropertyChanged();
                FilteredRoomTypes.Refresh();
            }
        }

        private RoomType? _selectedRoomType;
        public RoomType? SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                OnPropertyChanged();
            }
        }

        public RoomTypeManagerViewModel()
        {
            RoomTypes = new ObservableCollection<RoomType>(_dao.GetAll());
            FilteredRoomTypes = CollectionViewSource.GetDefaultView(RoomTypes);
            FilteredRoomTypes.Filter = _ => FilterByKeyword(_);
        }

        private bool FilterByKeyword(object item)
        {
            if (item is not RoomType rt) return false;
            return string.IsNullOrWhiteSpace(Keyword) ||
                   rt.RoomTypeName.Contains(Keyword, StringComparison.OrdinalIgnoreCase) ||
                   rt.TypeDescription.Contains(Keyword, StringComparison.OrdinalIgnoreCase);
        }

        // ───── Commands ─────
        public ICommand CreateCommand => new RelayCmd(_ => OpenDialogForCreate());
        public ICommand EditCommand => new RelayCmd(_ => OpenDialogForEdit(), _ => SelectedRoomType != null);
        public ICommand DeleteCommand => new RelayCmd(_ => DeleteSelected(), _ => SelectedRoomType != null);

        private void OpenDialogForCreate()
        {
            var fresh = new RoomType { RoomTypeName = "", TypeDescription = "", TypeNote = "" };
            var dlg = new RoomTypeDialog(fresh) { Title = "Create Room Type" };
            if (dlg.ShowDialog() == true)
            {
                _dao.Add(fresh);
                RoomTypes.Add(fresh);
            }
        }

        private void OpenDialogForEdit()
        {
            var clone = new RoomType
            {
                RoomTypeId = SelectedRoomType!.RoomTypeId,
                RoomTypeName = SelectedRoomType.RoomTypeName,
                TypeDescription = SelectedRoomType.TypeDescription,
                TypeNote = SelectedRoomType.TypeNote
            };

            var dlg = new RoomTypeDialog(clone) { Title = "Update Room Type" };
            if (dlg.ShowDialog() == true)
            {
                _dao.Update(clone);
                SelectedRoomType.RoomTypeName = clone.RoomTypeName;
                SelectedRoomType.TypeDescription = clone.TypeDescription;
                SelectedRoomType.TypeNote = clone.TypeNote;
                FilteredRoomTypes.Refresh();
            }
        }

        private void DeleteSelected()
        {
            if (MessageBox.Show("Are you sure you want to delete this room type?",
                                "Confirm delete", MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _dao.Delete(SelectedRoomType!.RoomTypeId);
                RoomTypes.Remove(SelectedRoomType);
            }
        }

        // ───── INotifyPropertyChanged ─────
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? p = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }

    // Reuse the existing RelayCmd from RoomManagerViewModel
}