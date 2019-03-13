using Probel.Gehova.ViewModels.Vm.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Probel.Gehova.Views.Views.Settings
{
    public sealed partial class AddPersonView : ContentDialog
    {
        #region Constructors

        public AddPersonView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public AddPersonViewModel ViewModel => DataContext as AddPersonViewModel;

        #endregion Properties

        private void OnChecked(object sender, RoutedEventArgs e) => ViewModel.CheckAdd();

        private void OnUnchecked(object sender, RoutedEventArgs e) => ViewModel.CheckAdd();
    }
}
