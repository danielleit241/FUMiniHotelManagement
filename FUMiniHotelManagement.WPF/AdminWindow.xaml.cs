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
        private void ManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow c = new();
            c.ShowDialog();
            this.Close();
        }

        private void ManageRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomWindow r = new();
            r.ShowDialog();
            this.Close();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FillBookingReservationGrid();
        }

        private void FillBookingReservationGrid()
        {
            BookingReservationDataGrid.ItemsSource = null;
            BookingReservationDataGrid.ItemsSource = _service.GetAllBookingReservations();
        }
    }
}
