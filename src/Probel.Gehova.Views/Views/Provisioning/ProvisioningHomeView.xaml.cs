using Probel.Gehova.ViewModels.Provisioning;
using Probel.Gehova.Views.Infrastructure;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Provisioning
{
    public sealed partial class ProvisioningHomeView : Page
    {
        #region Constructors

        public ProvisioningHomeView()
        {
            InitializeComponent();
            DataContext = IocFactory.ViewModel.ProvisioningHomeViewModel;
        }

        #endregion Constructors

        #region Properties

        public ProvisioningHomeViewModel ViewModel => DataContext as ProvisioningHomeViewModel;

        #endregion Properties

        #region Methods

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Refresh();
        }

        #endregion Methods
    }
}