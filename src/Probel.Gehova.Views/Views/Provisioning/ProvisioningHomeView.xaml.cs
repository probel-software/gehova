using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Probel.Gehova.ViewModels.Infrastructure;
using Probel.Gehova.ViewModels.Vm.Provisioning;
using Probel.Gehova.Views.Helpers;
using Probel.Gehova.Views.Infrastructure;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Provisioning
{
    public sealed partial class ProvisioningHomeView : Page
    {
        #region Fields

        private readonly IUserMessenger _messenger = new InAppMessenger();
        private readonly ResourceLoader _resources = new ResourceLoader("Messages");

        #endregion Fields

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

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddAbsenceView { DataContext = IocFactory.ViewModel.AbsenceViewModel };
            dialog.ViewModel.PersonId = ViewModel.SelectedPerson.Id;
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (dialog.ViewModel.HasBeenAdded) { _messenger.Say(_resources.GetString("Info_AbsenceAdded")); }
                else { _messenger.Say(_resources.GetString("Info_AbsenceNOTAdded")); }
                ViewModel.Refresh();
            }
        }

        private void OnDragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            // Prepare a string with one dragged item per line
            var items = new StringBuilder();
            foreach (dynamic item in e.Items) { items.Append($"{item.Id},"); }
            // Set the content of the DataPackage
            e.Data.SetText(items.ToString().Remove(items.Length - 1, 1));
            // As we want our Reference list to say intact, we only allow Copy
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            // Our list only accepts text
            //e.AcceptedOperation = (e.DataView.Contains(StandardDataFormats.StorageItems)) ? DataPackageOperation.Copy : DataPackageOperation.None;
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async Task OnDrop(DragEventArgs e, Action<IEnumerable<long>> onViewModel)
        { // This test is in theory not needed as we returned DataPackageOperation.None if
            // the DataPackage did not contained text. However, it is always better if each
            // method is robust by itself
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                // We need to take a Deferral as we won't be able to confirm the end
                // of the operation synchronously
                var def = e.GetDeferral();
                var s = await e.DataView.GetTextAsync();
                var items = (from it in s.Split(',')
                             select long.Parse(it));

                onViewModel(items);

                e.AcceptedOperation = DataPackageOperation.Move;
                def.Complete();
            }
        }

        private void OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            TbSearchBoxPerson.Text = args.SelectedItem?.ToString();

            var res = ViewModel.FindById(args.SelectedItem);
            LvPeople.SelectedItem = res;
            ViewModel.SelectedPerson = res;
            ViewModel.RefreshAbsencesCommand.TryExecute();
        }

        private void OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var result = ViewModel.FindByName(TbSearchBoxPerson.Text);
                sender.ItemsSource = result;
            }
        }

        private async void PickupRoundDestination_Drop(object sender, DragEventArgs e) => await OnDrop(e, items => ViewModel.AddToPickupRoundAndUpdate(items));

        private async void PickupRoundSource_Drop(object sender, DragEventArgs e) => await OnDrop(e, items => ViewModel.RemoveFromPickupRoundAndUpdate(items));

        private async void TeamDestination_Drop(object sender, DragEventArgs e) => await OnDrop(e, items => ViewModel.AddToTeamAndUpdate(items));

        private async void TeamSource_Drop(object sender, DragEventArgs e) => await OnDrop(e, items => ViewModel.RemoveFromTeamAndUpdate(items));

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Refresh();
        }

        #endregion Methods

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var person = (e.AddedItems.Count > 0) ? e.AddedItems[0] : null;

            if (ViewModel.RefreshAbsencesCommand.CanExecute(person))
            {
                ViewModel.RefreshAbsencesCommand.Execute(person);
            }
        }
    }
}
