using System.Windows;
using FUMiniHotelManagement.BLL.Services;
using FUMiniHotelManagement.DAL.Entities;

namespace FUMiniHotelManagement.WPF
{
    /// <summary>
    /// Interaction logic for RoomDetailsWindow.xaml
    /// </summary>
    public partial class RoomDetailsWindow : Window
    {
        public RoomInformation EditedRoom { get; set; } = null!;
        private readonly RoomService _roomService = new();
        private readonly RoomTypeService _roomTypeService = new();

        public RoomDetailsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();

            if (EditedRoom != null)
            {
                FillElements(EditedRoom);
                DetailWindowModeLabel.Text = "Update Room Information";
            }
            else
            {
                DetailWindowModeLabel.Text = "Create Room Information";
            }
            RoomIdTextBox.IsEnabled = false;
        }

        private void FillElements(RoomInformation x)
        {
            if (x == null)
                return;

            RoomIdTextBox.Text = x.RoomId.ToString();
            RoomNumberTextBox.Text = x.RoomNumber;
            RoomDetailDescriptionTextBox.Text = x.RoomDetailDescription;
            RoomMaxCapacityTextBox.Text = x.RoomMaxCapacity?.ToString();
            RoomStatusTextBox.Text = x.RoomStatus?.ToString();
            RoomPricePerDayTextBox.Text = x.RoomPricePerDay?.ToString();
            RoomTypeIdComboBox.SelectedValue = x.RoomTypeId;
        }

        private void FillComboBox()
        {
            RoomTypeIdComboBox.ItemsSource = _roomTypeService.GetRoomTypes();
            RoomTypeIdComboBox.DisplayMemberPath = "RoomTypeName";
            RoomTypeIdComboBox.SelectedValuePath = "RoomTypeId";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedValue = RoomTypeIdComboBox.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(selectedValue))
            {
                MessageBox.Show("Please select a valid Room Type.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var x = new RoomInformation
            {
                RoomNumber = RoomNumberTextBox.Text,
                RoomDetailDescription = RoomDetailDescriptionTextBox.Text,
                RoomMaxCapacity = int.Parse(RoomMaxCapacityTextBox.Text),
                RoomStatus = byte.Parse(RoomStatusTextBox.Text),
                RoomPricePerDay = decimal.Parse(RoomPricePerDayTextBox.Text),
                RoomTypeId = int.Parse(selectedValue)
            };

            if (EditedRoom == null)
            {
                _roomService.CreateRoom(x);
            }
            else
            {
                x.RoomId = EditedRoom.RoomId;
                _roomService.UpdateRoom(x);
            }
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to close this window?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                this.Close();
            }
        }
    }
}