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
    public sealed partial class ReceptionPrint : Page
    {
        #region Fields

        public static DependencyProperty DaysProperty = DependencyProperty.Register(
            "Days",
            typeof(IEnumerable<DayModel>),
            typeof(ReceptionPrint),
            null);

        public static DependencyProperty ReceptionNameProperty = DependencyProperty.Register(
            "ReceptionName",
            typeof(string),
            typeof(ReceptionPrint),
            null);

        public static DependencyProperty WeekAsTextProperty = DependencyProperty.Register(
            "WeekAsText",
            typeof(string),
            typeof(TeamPrint),
            null);

        #endregion Fields

        #region Constructors

        public ReceptionPrint() => InitializeComponent();

        #endregion Constructors

        #region Properties

        public IEnumerable<DayModel> Days
        {
            get => (IEnumerable<DayModel>)GetValue(DaysProperty);
            set => SetValue(DaysProperty, value);
        }

        public string ReceptionName
        {
            get => (string)GetValue(ReceptionNameProperty);
            set => SetValue(ReceptionNameProperty, value);
        }

        public string WeekAsText
        {
            get => (string)GetValue(WeekAsTextProperty);
            set => SetValue(WeekAsTextProperty, value);
        }

        #endregion Properties
    }
}