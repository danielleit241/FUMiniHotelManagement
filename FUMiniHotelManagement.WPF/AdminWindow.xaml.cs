using System.Windows;
using FUMiniHotelManagement.BLL.Services;

namespace FUMiniHotelManagement.WPF
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly BookingService _service = new();

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to quit?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void ManageRoomButton_Click(object sender, RoutedEventArgs e)
        {
            RoomWindow r = new();
            r.ShowDialog();
        }

        private void ManageCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow c = new();
            c.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillBokingHistory();
            TotalMoneyLable.Text = "$" + Math.Round((double)_service.GetTotalPrice() * 1000) / 1000;
            TotalRoomBookedLable.Text = "" + _service.GetTotalBookingCount();
        }

        private void FillBokingHistory()
        {
            AdminBookingReservationHistoryDataGrids.ItemsSource = null;
            AdminBookingReservationHistoryDataGrids.ItemsSource = _service.GetAllBookingReservations();
        }
    }
}
