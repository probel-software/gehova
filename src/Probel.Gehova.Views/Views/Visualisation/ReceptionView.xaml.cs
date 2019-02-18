using Probel.Gehova.Business.Models;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Visualisation
{
    public sealed partial class ReceptionView : UserControl
    {
        #region Constructors

        public ReceptionView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        public static DependencyProperty DaysProperty = DependencyProperty.Register(
                "Days",
                typeof(IEnumerable<DayModel>),
                typeof(ReceptionView),
                null
            );

        public IEnumerable<DayModel> Days
        {
            get => (IEnumerable<DayModel>)GetValue(DaysProperty);
            set => SetValue(DaysProperty, value);
        }
    }
}