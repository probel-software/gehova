using Microsoft.Toolkit.Uwp.Helpers;
using Probel.Gehova.ViewModels.Infrastructure;
using Probel.Gehova.ViewModels.Vm.Visualisation;
using Probel.Gehova.Views.Infrastructure;
using Probel.Gehova.Views.Views.PrintDocument;
using System;
using Windows.ApplicationModel.Resources;
using Windows.Graphics.Printing;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Probel.Gehova.Views.Views.Visualisation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlannerReceptionView : Page
    {
        #region Fields

        private readonly IUserMessenger _messenger = new InAppMessenger();

        private PrintHelper _printHelper;

        #endregion Fields

        #region Constructors

        public PlannerReceptionView()
        {
            InitializeComponent();
            DataContext = IocFactory.ViewModel.ReceptionPlannerViewModel;
            CdpWeekSelector.Date = DateTime.Today;
        }

        #endregion Constructors

        #region Properties

        private ResourceLoader Txt => new ResourceLoader("Messages");
        public PlannerReceptionViewModel ViewModel => DataContext as PlannerReceptionViewModel;

        #endregion Properties

        #region Methods

        private async void OnPrint(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var opt = new PrintHelperOptions
            {
                Orientation = PrintOrientation.Landscape
            };

            _printHelper = new PrintHelper(PrintCanvas, opt);
            _printHelper.OnPrintSucceeded += OnPrintSucceeded;
            _printHelper.OnPrintFailed += OnPrintFailed;

            foreach (var receptionGroup in ViewModel.ReceptionGroups)
            {
                foreach (var reception in receptionGroup.Receptions)
                {
                    var doc = new ReceptionPrint
                    {
                        ReceptionName = reception.ReceptionName,
                        WeekAsText = ViewModel.DisplayedWeekAsText,
                        Days = reception.Days
                    };
                    _printHelper.AddFrameworkElementToPrint(doc);
                }
            }

            await _printHelper.ShowPrintUIAsync(Txt.GetString("Title_PrintReceptions"));
        }

        private void OnPrintFailed()
        {
            _printHelper?.Dispose();
            _messenger.Say($"Printing miserably failed...");
        }

        private void OnPrintSucceeded()
        {
            _printHelper?.Dispose();
            _messenger.Say($"Printing proudly succeed...");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) => ViewModel.Refresh();

        #endregion Methods
    }
}