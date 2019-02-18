using Probel.Gehova.ViewModels.Infrastructure;
using Probel.Gehova.ViewModels.Vm.Visualisation;
using Probel.Gehova.Views.Infrastructure;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Visualisation
{
    public sealed partial class TeamPlannerView : Page
    {
        #region Constructors

        public TeamPlannerView()
        {
            InitializeComponent();
            DataContext = IocFactory.ViewModel.TeamPlannerViewModel;
            ViewModel.Messenger = new InAppMessenger(InAppNotification);
            CdpWeekSelector.Date = DateTime.Today;
        }

        #endregion Constructors

        #region Properties

        public IMessenger Messenger { get; set; }

        public TeamPlannerViewModel ViewModel => DataContext as TeamPlannerViewModel;

        #endregion Properties

        #region Methods

        protected override void OnNavigatedTo(NavigationEventArgs e) => ViewModel.Refresh();

        #endregion Methods
    }
}