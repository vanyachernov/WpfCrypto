using market.ViewModels;
using System.Windows.Controls;

namespace market.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для Converter.xaml
    /// </summary>
    public partial class Converter : UserControl
    {
        private readonly ConverterViewModel _converterModel;
        
        public Converter()
        {
            InitializeComponent();
            _converterModel = new ConverterViewModel();
        }

        private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _converterModel.LoadCurrenciesAsync();
            DataContext = _converterModel;
        }
    }
}
