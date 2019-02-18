﻿using Probel.Gehova.ViewModels.Vm.Visualisation;
using Probel.Gehova.Views.Infrastructure;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Probel.Gehova.Views.Views.Visualisation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlaReceptionPlannerViewnerView : Page
    {
        #region Constructors

        public PlaReceptionPlannerViewnerView()
        {
            InitializeComponent();
            DataContext = IocFactory.ViewModel.PlannerViewModel;
            ViewModel.Messenger = new InAppMessenger(InAppNotification);
            CdpWeekSelector.Date = DateTime.Today;
        }

        #endregion Constructors

        #region Properties

        public ReceptionPlannerView ViewModel => DataContext as ReceptionPlannerView;

        #endregion Properties

        #region Methods

        protected override void OnNavigatedTo(NavigationEventArgs e) => ViewModel.Refresh();

        #endregion Methods
    }
}