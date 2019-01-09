using Probel.Gehova.ViewModels.Vm.Settings;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Probel.Gehova.Views.Views.Settings
{
    public sealed partial class AddWithNameView : ContentDialog
    {
        #region Constructors

        public AddWithNameView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public IAddWithNameViewModel ViewModel
        {
            get => DataContext as IAddWithNameViewModel;
            set => DataContext = value;
        }

        #endregion Properties
    }
}