using Probel.Gehova.ViewModels.Provisioning;
using Probel.Gehova.Views.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Provisioning
{
    public sealed partial class ProvisioningHomeView : Page
    {
        #region Fields


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
        private async void PickupRoundDestination_Drop(object sender, DragEventArgs e) => await OnDrop(e, items => ViewModel.AddToPickupRoundAndUpdate(items));


        private async void PickupRoundSource_Drop(object sender, DragEventArgs e) => await OnDrop(e, items => ViewModel.RemoveFromPickupRoundAndUpdate(items));

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Refresh();
        }

        #endregion Methods




        private async void TeamDestination_Drop(object sender, DragEventArgs e) => await OnDrop(e, items => ViewModel.AddToTeamAndUpdate(items));
        private async void TeamSource_Drop(object sender, DragEventArgs e) => await OnDrop(e, items => ViewModel.RemoveFromTeamAndUpdate(items));
    }
}