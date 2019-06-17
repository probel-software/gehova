using Probel.Gehova.ViewModels.Vm.AppSettings;
using Probel.Gehova.Views.Infrastructure;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Probel.Gehova.Views.Views.AppSettings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ApplicationSettingsView : Page
    {
        #region Constructors

        public ApplicationSettingsView()
        {
            InitializeComponent();
            DataContext = IocFactory.ViewModel.ApplicationSettingsViewModel;
        }

        #endregion Constructors

        #region Properties

        public ApplicationSettingsViewModel ViewModel => DataContext as ApplicationSettingsViewModel;

        #endregion Properties
    }
}
