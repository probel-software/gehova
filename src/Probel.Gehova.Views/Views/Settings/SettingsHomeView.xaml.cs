﻿using Microsoft.Toolkit.Uwp.UI.Controls;
using Probel.Gehova.ViewModels.Infrastructure;
using Probel.Gehova.ViewModels.ViewModelBuilders;
using Probel.Gehova.ViewModels.Vm.Settings;
using Probel.Gehova.Views.Infrastructure;
using Probel.Gehova.Views.Views.Settings;
using System;
using System.Linq;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Administration
{
    public sealed partial class SettingsHomeView : Page
    {
        #region Fields

        private readonly IUserMessenger _messenger = new InAppMessenger();
        private readonly ResourceLoader _resources = new ResourceLoader("Messages");

        #endregion Fields

        #region Constructors

        public SettingsHomeView()
        {
            InitializeComponent();
            DataContext = IocFactory.ViewModel.SettingsHomeViewModel;
        }

        #endregion Constructors

        #region Properties

        private SettingsHomeViewModel ViewModel => DataContext as SettingsHomeViewModel;

        #endregion Properties

        #region Methods

        private async void ClickOnAddPerson(object sender, RoutedEventArgs e)
        {
            var vm = IocFactory.ViewModel.AddPersonViewModel;
            ViewModel.HandDown(vm);

            var dialog = new AddPersonView { DataContext = vm };
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var p = dialog.ViewModel;
                _messenger.Say(string.Format(_resources.GetString("Info_PersonAdded"), $"{p?.FirstName} {p?.LastName}"));
                ViewModel.Refresh();
            }
        }

        private async void ClickOnAddPickupRound(object sender, RoutedEventArgs e)
        {
            var vm = IocFactory.ViewModel.AddPickupRoundViewModel;

            var dialog = new AddWithNameView { DataContext = vm };
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var p = dialog.ViewModel;
                _messenger.Say(string.Format(_resources.GetString("Info_PickupRoundCreated"), p?.Name));
                ViewModel.Refresh();
            }
        }

        private async void ClickOnAddTeam(object sender, RoutedEventArgs e)
        {
            var vm = IocFactory.ViewModel.AddTeamViewModel;

            var dialog = new AddWithNameView { DataContext = vm };
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var p = dialog.ViewModel;
                _messenger.Say(string.Format(_resources.GetString("Info_TeamCreated"), p?.Name));
                ViewModel.Refresh();
            }
        }

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

        private async void OnDeleteCurrentPickupRound(object sender, RoutedEventArgs e) => await DialogRemovePickupRound.ShowAsync();

        private async void OnDeleteCurrentTeam(object sender, RoutedEventArgs e) => await DialogRemoveTeam.ShowAsync();

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count() > 0)
            {
                if ((e.AddedItems[0]?.ToString() ?? "") != TbSearchBoxPerson.Text) { TbSearchBoxPerson.Text = string.Empty; }
                ViewModel.RefreshSelectedPerson(e.AddedItems[0]);
            }
        }

        private void OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            TbSearchBoxPerson.Text = args.SelectedItem?.ToString();

            var res = ViewModel.FindById(args.SelectedItem);
            MasterDetailPeople.SelectedItem = res;
        }

        private void OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var result = ViewModel.FindByName(TbSearchBoxPerson.Text);
                sender.ItemsSource = result;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) => ViewModel.Refresh();

        #endregion Methods
    }
}