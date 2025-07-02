using System.Windows;
using FUMiniHotelManagement.BLL.Services;
using FUMiniHotelManagement.DAL.Entities;

namespace FUMiniHotelManagement.WPF
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public Customer? UserLogin { get; set; }
        private readonly UserService userService = new();
        private readonly BookingService bookingService = new();

        public UserWindow()
        {
            InitializeComponent();
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

            BookingReservationHistoryDataGrids.ItemsSource = bookingService.GetBookingReservationsByCustomerId(UserLogin.CustomerId);
        }

        private void UpdateButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var comfirmed = MessageBox.Show("Are you sure you want to update your profile?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (comfirmed != MessageBoxResult.Yes)
                {
                    return;
                }
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
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
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

        private void QuitButton_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
