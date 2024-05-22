using System.Windows;
using market.Models;

namespace market.Views.Windows
{
    public partial class CoinDetails : Window
    {
        private Currency _currency;

        public CoinDetails(Currency currentCoin)
        {
            InitializeComponent();
            _currency = currentCoin;
            DataContext = _currency;
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
