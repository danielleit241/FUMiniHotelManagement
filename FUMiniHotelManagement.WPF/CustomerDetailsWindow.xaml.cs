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
        public Customer EditedCus { get; set; } = null;
        public CustomerDetailsWindow()
        {
            InitializeComponent();
        }
        private UserService _service = new();
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Logic to save customer details
            MessageBox.Show("Customer details saved successfully!");
            this.Close();
        }

        private bool ValidateElement()
        {
            bool isValid = true;

            if (CustomerFullName.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Air Conditioner Name is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FillElememts(EditedCus);
            //fill label cho rõ màn hình làm gì : Add hay Edit
            if (EditedCus != null)
            {
                CustomerDetailWindowLabel.Text = "Edit Customer";
            }
            else
            {
                CustomerDetailWindowLabel.Text = "Add Customer";
                CustomerID.IsEnabled = false;
                //FillIdIdentity();
            }
        }

        //private void FillIdIdentity()
        //{
        //    int maxId = _service.GetMaxCusId() + 1;
        //    CustomerID.Text = maxId.ToString();
        //    CustomerID.IsEnabled = false;
        //}

        private void FillElememts(Customer x)
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSaveCus_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateElement())
                return;

            Customer c = new Customer();
            //c.CustomerId = int.Parse(CustomerID.Text);
            c.CustomerFullName = CustomerFullName.Text;
            c.Telephone = Telephone.Text;
            c.EmailAddress = EmailAddress.Text;
            c.CustomerBirthday = DateOfBirth.SelectedDate.HasValue
            ? DateOnly.FromDateTime(DateOfBirth.SelectedDate.Value)
            : (DateOnly?)null;
            c.CustomerStatus = byte.Parse(CustomerStatus.Text);
            c.Password = Password.Text;


            if(EditedCus == null)
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
