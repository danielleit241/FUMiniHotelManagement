using System.IO;
using System.Windows;
using FUMiniHotelManagement.BLL.Services;
using Microsoft.Extensions.Configuration;

namespace FUMiniHotelManagement.WPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private UserService service = new();
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailAddressTextBox.Text;
            string password = PasswordPasswordBox.Password;

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email address.", "Field required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your password.", "Field required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true).Build();

            var customer = service.Login(email, password);

            if (customer != null)
            {
                UserWindow user = new();
                user.UserLogin = customer;
                user.Show();
                this.Hide();
            }
            else if (email.Equals(configuration["DefaultAccount:Email"]) && password.Equals(configuration["DefaultAccount:Password"]))
            {
                //set authenticate
                AdminWindow admin = new();
                admin.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect email or password. Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
