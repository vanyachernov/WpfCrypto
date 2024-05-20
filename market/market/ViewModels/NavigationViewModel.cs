using market.Utilities;
using System.Windows.Input;

namespace market.ViewModels
{
    class NavigationViewModel : ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand CurrenciesCommand { get; set; }
        public ICommand ConverterCommand { get; set; }

        private void Currencies(object obj) => CurrentView = new CurrenciesViewModel();
        private void Converter(object obj) => CurrentView = new ConverterViewModel();

        public NavigationViewModel()
        {
            CurrenciesCommand = new RelayCommand(Currencies);
            ConverterCommand = new RelayCommand(Converter);

            // Startup page (top 10 currencies by its rank)
            CurrentView = new CurrenciesViewModel();
        }
    }
}
