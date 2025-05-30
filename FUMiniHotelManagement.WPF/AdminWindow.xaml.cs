using FUMiniHotelManagement.BLL.Services;
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
            Application.Current.Shutdown();
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
            TotalMoneyLable.Content = "Total money: " + _service.GetTotalPrice();
            TotalRoomBookedLable.Content = "Total Rooms Booked: " + _service.GetTotalBookingCount();
        }

        private void FillBokingHistory()
        {
            AdminBookingReservationHistoryDataGrids.ItemsSource = null;
            AdminBookingReservationHistoryDataGrids.ItemsSource = _service.GetAllBookingReservations();
        }
    }
}
