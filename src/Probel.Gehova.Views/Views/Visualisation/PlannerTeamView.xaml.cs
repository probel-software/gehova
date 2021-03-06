﻿using Microsoft.Toolkit.Uwp.Helpers;
using Probel.Gehova.Business.Models;
using Probel.Gehova.ViewModels.Infrastructure;
using Probel.Gehova.ViewModels.Vm.Visualisation;
using Probel.Gehova.Views.Infrastructure;
using Probel.Gehova.Views.Views.PrintDocument;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Resources;
using Windows.Graphics.Printing;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Visualisation
{
    public sealed partial class PlannerTeamView : Page
    {
        #region Fields

        private readonly IUserMessenger _messenger = new InAppMessenger();
        private PrintHelper _printHelper;

        #endregion Fields

        #region Constructors

        public PlannerTeamView()
        {
            InitializeComponent();
            DataContext = IocFactory.ViewModel.TeamPlannerViewModel;
            CdpWeekSelector.Date = DateTime.Today;
        }

        #endregion Constructors

        #region Properties

        private ResourceLoader Txt => new ResourceLoader("Messages");
        public IUserMessenger Messenger { get; set; }

        public PlannerTeamViewModel ViewModel => DataContext as PlannerTeamViewModel;

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

            var doc = new TeamPrint
            {
                WeekAsText = ViewModel.DisplayedWeekAsText,
                Days = GroupList.ItemsSource as IEnumerable<DayModel>
            };

            _printHelper.AddFrameworkElementToPrint(doc);

            await _printHelper.ShowPrintUIAsync(Txt.GetString("Title_PrintPickupRounds"));
        }


        private void OnPrintFailed()
        {
            _printHelper?.Dispose();
            _messenger.Warn(Txt.GetString("Error_PrintingFailed"));
        }

        private void OnPrintSucceeded()
        {
            _printHelper?.Dispose();
            _messenger.Say(Txt.GetString("Info_Printing"));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) => ViewModel.Refresh();

        #endregion Methods
    }
}