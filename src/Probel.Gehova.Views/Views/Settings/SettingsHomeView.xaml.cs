using Microsoft.Toolkit.Uwp.UI.Controls;
using Probel.Gehova.ViewModels.Settings;
using Probel.Gehova.Views.Infrastructure;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Administration
{
    public sealed partial class SettingsHomeView : Page
    {
        #region Constructors

        public SettingsHomeView()
        {
            InitializeComponent();
            DataContext = IocFactory.ViewModel.SettingsHomeViewModel;
            ViewModel.Messenger = new InAppMessenger(InAppNotification);
        }

        #endregion Constructors

        #region Properties

        private SettingsHomeViewModel ViewModel => DataContext as SettingsHomeViewModel;

        #endregion Properties

        #region Methods

        private void DgPickupRounds_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ViewModel.UpdatePickupRoundCommand.Execute(DgPickupRounds.SelectedItem);
            }
        }

        private void DgTeams_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ViewModel.UpdateTeamCommand.Execute(DgTeams.SelectedItem);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) => ViewModel.Refresh();

        #endregion Methods
    }
}