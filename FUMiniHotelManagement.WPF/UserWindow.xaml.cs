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
using FUMiniHotelManagement.DAL.Entities;
using FUMiniHotelManagement.BLL.Services;

namespace FUMiniHotelManagement.WPF
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public Customer? UserLogin { get; set; }  
        private readonly UserService userService;

        public UserWindow()
        {
            InitializeComponent();
            userService = new UserService();
            UpdateButton.Click += UpdateButton_Click;
            QuitButton.Click += QuitButton_Click;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserLogin != null)
            {
                LoadUserProfile();
                LoadBookingHistory();
                
            }
            else
            {
                MessageBox.Show("User is not logged in.");
                this.Close();
            }
        }

        private void LoadUserProfile()
        {
            if (UserLogin == null)
            {
                MessageBox.Show("User is not logged in.");
                return;
            }

            FullNameTextBox.Text = UserLogin.CustomerFullName ?? "";
            TelephoneTextBox.Text = UserLogin.Telephone ?? "";
            EmailAddressTextBox.Text = UserLogin.EmailAddress ?? "";
            BirthDayTextBox.Text = UserLogin.CustomerBirthday?.ToString("yyyy-MM-dd") ?? "";
        }

        private void LoadBookingHistory()
        {
            if (UserLogin == null)
            {
                MessageBox.Show("User is not logged in.");
                return;
            }

            var bookingList = userService.GetBookingReservationsByCustomerId(UserLogin.CustomerId);
            BookingReservationHistoryDataGrids.ItemsSource = bookingList;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserLogin == null)
                {
                    MessageBox.Show("User is not logged in.");
                    return;
                }

                UserLogin.CustomerFullName = FullNameTextBox.Text.Trim();
                UserLogin.Telephone = TelephoneTextBox.Text.Trim();
                UserLogin.EmailAddress = EmailAddressTextBox.Text.Trim();

                if (DateTime.TryParse(BirthDayTextBox.Text.Trim(), out DateTime birthday))
                {
                    UserLogin.CustomerBirthday = DateOnly.FromDateTime(birthday);
                }
                else
                {
                    MessageBox.Show("Invalid birthday format. Use yyyy-MM-dd");
                    return;
                }

                userService.UpdateCustomer(UserLogin);
                MessageBox.Show("Profile updated successfully!");
                LoadUserProfile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating profile: " + ex.Message);
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
