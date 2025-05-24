using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using FUMiniHotelManagement.BLL.Services;
using FUMiniHotelManagement.DAL.Entities;

namespace FUMiniHotelManagement.WPF
{
    /// <summary>
    /// Interaction logic for RoomWindow.xaml
    /// </summary>
    public partial class RoomWindow : Window
    {
        private readonly RoomService _service = new();

        public RoomWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillGridData(_service.GetRooms());
        }

        private void FillGridData(List<RoomInformation> rooms)
        {
            RoomsDataGrid.ItemsSource = null;
            RoomsDataGrid.ItemsSource = rooms;
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            RoomDetailsWindow roomDetailsWindow = new();
            roomDetailsWindow.ShowDialog();
            FillGridData(_service.GetRooms());
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = GetSelectedRoom();
            if (selectedRoom == null)
            {
                return;
            }
            RoomDetailsWindow roomDetailsWindow = new();
            roomDetailsWindow.EditedRoom = selectedRoom;
            roomDetailsWindow.ShowDialog();
            FillGridData(_service.GetRooms());
        }

        private RoomInformation? GetSelectedRoom()
        {
            RoomInformation? selectedRoom = RoomsDataGrid.SelectedItem as RoomInformation;
            if (selectedRoom == null)
            {
                MessageBox.Show("Please selected a room to update.", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }

            return selectedRoom;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = GetSelectedRoom();
            if (selectedRoom == null)
            {
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this room?", "Delete Room", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                selectedRoom.RoomStatus = 0;
                _service.DeletableRoom(selectedRoom);
                FillGridData(_service.GetRooms());
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            decimal? price = null;
            if (decimal.TryParse(RoomPriceTextBox.Text, out var parsedPrice))
            {
                price = parsedPrice;
            }

            var result = _service.SearchRoomsByDesOrPrice(RoomDescriptionTextBox.Text.ToString(), price);

            FillGridData(result);
        }
    }
}
