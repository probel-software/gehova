using Probel.Gehova.Business.Models;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Visualisation
{
    public sealed partial class DayView : UserControl
    {
        #region Fields

        public static DependencyProperty TeamsProperty = DependencyProperty.Register(
            "Items",
            typeof(IEnumerable<GroupModel>),
            typeof(DayView),
            null
            );

        #endregion Fields

        #region Constructors

        public DayView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public IEnumerable<GroupModel> Items
        {
            get => (IEnumerable<GroupModel>)GetValue(TeamsProperty);
            set => SetValue(TeamsProperty, value);
        }

        #endregion Properties
    }
}