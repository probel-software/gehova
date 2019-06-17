using System.Collections.Generic;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Views.Infrastructure;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Visualisation
{
    public sealed partial class DayView : UserControl
    {
        #region Fields

        public static DependencyProperty DayFontSizeProperty = DependencyProperty.Register(
            "DayFontSize",
            typeof(double),
            typeof(TeamView),
            new PropertyMetadata(18d));

        public static DependencyProperty TeamsProperty = DependencyProperty.Register(
            "Items",
            typeof(IEnumerable<GroupModel>),
            typeof(DayView),
            null
            );

        public static DependencyProperty TeamMinWidthProperty = DependencyProperty.Register(
            "TeamMinWidth",
            typeof(double),
            typeof(DayView),
            new PropertyMetadata(200d)
            );

        #endregion Fields

        #region Constructors

        public DayView()
        {
            InitializeComponent();
            var s = IocFactory.Settings;
            DayFontSize = s.DayFontSize;
            TeamMinWidth = s.TeamMinWidth;
        }

        #endregion Constructors

        #region Properties

        public double DayFontSize
        {
            get => (double)GetValue(DayFontSizeProperty);
            set => SetValue(DayFontSizeProperty, value);
        }

        public IEnumerable<GroupModel> Items
        {
            get => (IEnumerable<GroupModel>)GetValue(TeamsProperty);
            set => SetValue(TeamsProperty, value);
        }

        public double TeamMinWidth
        {
            get => (double)GetValue(TeamMinWidthProperty);
            set => SetValue(TeamMinWidthProperty, value);
        }

        #endregion Properties
    }
}
