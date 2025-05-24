using System.Windows;

namespace FUMiniHotelManagement.WPF
{
    /// <summary>
    /// Interaction logic for CustomerDetailsWindow.xaml
    /// </summary>
    public partial class CustomerDetailsWindow : Window
    {
        public CustomerDetailsWindow()
        {
            InitializeComponent();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Logic to save customer details
            MessageBox.Show("Airco details saved successfully!");
            this.Close();
        }
    }
}
