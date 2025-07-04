﻿using System.Windows;
using FUMiniHotelManagement.BLL.Services;
using FUMiniHotelManagement.DAL.Entities;

namespace FUMiniHotelManagement.WPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private UserService _service = new();
        public CustomerWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FillCustomersGrid();
        }

        private void FillCustomersGrid()
        {
            CustomersDataGrid.ItemsSource = null;
            CustomersDataGrid.ItemsSource = _service.GetCustomers();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Are you sure to quit?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var result = _service.SearchCustomerByFullNameOrTelephone(FullNameTextBox.Text, TelephoneQuantityTextBox.Text);

            CustomersDataGrid.ItemsSource = null;
            CustomersDataGrid.ItemsSource = result;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Customer? selected = CustomersDataGrid.SelectedItem as Customer;

            if (selected == null)
            {
                MessageBox.Show("Please select a customer to delete.", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult answer = MessageBox.Show($"Are you sure to delete {selected.CustomerFullName}?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                selected.CustomerStatus = 0;
                _service.DeleteCustomer(selected);
                FillCustomersGrid();
            }
            else
            {
                selected = null;
                FillCustomersGrid();
                return;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetailsWindow c = new();
            c.ShowDialog();

            FillCustomersGrid();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Customer? selected = CustomersDataGrid.SelectedItem as Customer;
            if (selected == null)
            {
                MessageBox.Show("Please select a customer to update.", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            CustomerDetailsWindow c = new();
            c.EditedCus = selected;
            c.ShowDialog();

            FillCustomersGrid();
        }
    }
}
