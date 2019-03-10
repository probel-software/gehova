using Probel.Gehova.Business.Models;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Probel.Gehova.Views.Views.PrintDocument
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TeamPrint : Page
    {
        #region Fields

        public static DependencyProperty DaysProperty = DependencyProperty.Register(
            "Days",
            typeof(IEnumerable<DayModel>),
            typeof(TeamPrint),
            null);

        public static DependencyProperty WeekAsTextProperty = DependencyProperty.Register(
            "WeekAsText",
            typeof(string),
            typeof(TeamPrint),
            null);

        #endregion Fields

        #region Constructors

        public TeamPrint() => InitializeComponent();

        #endregion Constructors

        #region Properties

        public IEnumerable<DayModel> Days
        {
            get => (IEnumerable<DayModel>)GetValue(DaysProperty);
            set => SetValue(DaysProperty, value);
        }

        public string WeekAsText
        {
            get => (string)GetValue(WeekAsTextProperty);
            set => SetValue(WeekAsTextProperty, value);
        }

        #endregion Properties
    }
}