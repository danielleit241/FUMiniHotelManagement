using System.Windows;
using System.Windows.Media;
using FUMiniHotelManagement.BLL.Services;
using FUMiniHotelManagement.DAL.Entities;
using Microsoft.IdentityModel.Tokens;

namespace FUMiniHotelManagement.WPF
{
    /// <summary>
    /// Interaction logic for CustomerDetailsWindow.xaml
    /// </summary>
    public partial class CustomerDetailsWindow : Window
    {
        public Customer? EditedCus { get; set; }
        private UserService _service = new();

        public CustomerDetailsWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FillElements(EditedCus);
            // Set title depending on mode
            if (EditedCus != null)
            {
                CustomerDetailWindowLabel.Text = "Edit Customer";
            }
            else
            {
                CustomerDetailWindowLabel.Text = "Add Customer";
                CustomerID.IsEnabled = false;
            }
        }

        private void FillElements(Customer? x)
        {
            if (x == null)
                return;
            CustomerID.Text = x.CustomerId.ToString();
            CustomerID.IsEnabled = false;
            CustomerFullName.Text = x.CustomerFullName;
            Telephone.Text = x.Telephone;
            EmailAddress.Text = x.EmailAddress;
            DateOfBirth.SelectedDate = x.CustomerBirthday.HasValue
                ? x.CustomerBirthday.Value.ToDateTime(new TimeOnly(0, 0))
                : (DateTime?)null;
            CustomerStatus.Text = x.CustomerStatus.ToString();
            Password.Text = x.Password;
        }

        private bool ValidateElement()
        {
            bool isValid = true;

            if (CustomerFullName.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Customer Name is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CustomerFullName.BorderBrush = Brushes.Red;
                isValid = false;
            }
            else if (Telephone.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Telephone is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Telephone.BorderBrush = Brushes.Red;
                isValid = false;
            }
            else if (EmailAddress.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Email Address is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                EmailAddress.BorderBrush = Brushes.Red;
                isValid = false;
            }
            else if (DateOfBirth.SelectedDate == null)
            {
                MessageBox.Show("Date of Birth is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DateOfBirth.BorderBrush = Brushes.Red;
                isValid = false;
            }
            else if (Password.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Password is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Password.BorderBrush = Brushes.Red;
                isValid = false;
            }

            return isValid;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Are you sure to close?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
                return;

            this.Close();
        }

        private void btnSaveCus_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateElement())
                return;

            Customer c = new Customer
            {
                CustomerFullName = CustomerFullName.Text,
                Telephone = Telephone.Text,
                EmailAddress = EmailAddress.Text,
                CustomerBirthday = DateOfBirth.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(DateOfBirth.SelectedDate.Value)
                    : (DateOnly?)null,
                CustomerStatus = byte.Parse(CustomerStatus.Text),
                Password = Password.Text
            };

            if (EditedCus == null)
            {
                _service.CreateCustomer(c);
                MessageBox.Show("Customer added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                c.CustomerId = EditedCus.CustomerId;
                _service.UpdateCustomer(c);
                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            this.Close();
        }
    }
}