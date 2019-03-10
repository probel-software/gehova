using Microsoft.Toolkit.Uwp.UI.Controls;
using Probel.Gehova.ViewModels.Vm;
using Probel.Gehova.Views.Infrastructure;
using Probel.Gehova.Views.Views.Administration;
using Probel.Gehova.Views.Views.AppSettings;
using Probel.Gehova.Views.Views.Provisioning;
using Probel.Gehova.Views.Views.Visualisation;
using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Probel.Gehova.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Fields

        private static MainPage s_current;
        private readonly InAppMessenger _messenger;

        #endregion Fields

        #region Constructors

        public MainPage()
        {
            InitializeComponent();
            DataContext = IocFactory.ViewModel.MainViewModel;

            s_current = this;
            _messenger = new InAppMessenger();
        }

        #endregion Constructors

        #region Properties

        private MainViewModel ViewModel => DataContext as MainViewModel;

        internal static InAppNotification Messenger => s_current.InAppNotification;

        #endregion Properties

        #region Methods

        private void OnRootNavigation(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var nav = Navigator.GetInstance(contentFrame);
            Type destination;

            if (args.IsSettingsSelected) { destination = typeof(ApplicationSettingsView); }
            else if (args.SelectedItem == DataSettingView) { destination = typeof(SettingsHomeView); }
            else if (args.SelectedItem == PlannerView) { destination = typeof(PlannerReceptionView); }
            else if (args.SelectedItem == PickupRoundView) { destination = typeof(PlannerPickupRoundView); }
            else if (args.SelectedItem == ProvisioningView) { destination = typeof(ProvisioningHomeView); }
            else if (args.SelectedItem == TeamsView) { destination = typeof(PlannerTeamView); }
            else { throw new NotSupportedException($"Menu '{args.SelectedItem.GetType()}' is not yet supported"); }

            nav.Navigate(destination);
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel?.ExecuteUpdate();
            ViewModel?.LoadDefaultWeek();
        }

        #endregion Methods
    }
}
