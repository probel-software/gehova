using Probel.Gehova.ViewModels.Visualisation;
using Probel.Gehova.Views.Infrastructure;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Probel.Gehova.Views.Views.Visualisation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VisualisationHomeView : Page
    {
        #region Constructors

        public VisualisationHomeView()
        {
            InitializeComponent();
            DataContext = IocFactory.ViewModel.VisualisationHomeViewModel;
            ViewModel.Messenger = new InAppMessenger(InAppNotification);
        }

        #endregion Constructors

        #region Properties

        public VisualisationHomeViewModel ViewModel => DataContext as VisualisationHomeViewModel;

        #endregion Properties

        #region Methods

        protected override void OnNavigatedTo(NavigationEventArgs e) => ViewModel.Refresh();

        #endregion Methods
    }
}