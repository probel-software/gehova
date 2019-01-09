using Probel.Gehova.ViewModels.Vm.Provisioning;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Probel.Gehova.Views.Views.Provisioning
{
    public sealed partial class AddAbsenceView : ContentDialog
    {
        #region Constructors

        public AddAbsenceView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public AddAbsenceViewModel ViewModel => DataContext as AddAbsenceViewModel;

        #endregion Properties
    }
}