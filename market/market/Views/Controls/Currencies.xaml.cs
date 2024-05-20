using market.ViewModels;
using System.Windows.Controls;

namespace market.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для Currencies.xaml
    /// </summary>
    public partial class Currencies : UserControl
    {
        private readonly CurrenciesViewModel _currenciesModel;

        public Currencies()
        {
            InitializeComponent();
            _currenciesModel = new CurrenciesViewModel();
        }

        private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _currenciesModel.LoadCurrenciesAsync();
        }
    }
}
