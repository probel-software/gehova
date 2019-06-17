using System.Collections.Generic;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Views.Infrastructure;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Visualisation
{
    public sealed partial class TeamView : UserControl
    {
        #region Fields

        private DependencyProperty PeopleProperty = DependencyProperty.Register(
            "People",
            typeof(IEnumerable<PersonDisplayModel>),
            typeof(TeamView),
            null);

        public static DependencyProperty ContentHeightProperty = DependencyProperty.Register(
            "ContentHeight",
            typeof(double),
            typeof(TeamView),
            new PropertyMetadata(35d));

        public static DependencyProperty ContentIconVisibilityProperty = DependencyProperty.Register(
            "ContentIconVisibility",
            typeof(double),
            typeof(TeamView),
            new PropertyMetadata(true));

        public static DependencyProperty ContentTextSizeProperty = DependencyProperty.Register(
            "ContentTextSize",
            typeof(double),
            typeof(TeamView),
            new PropertyMetadata(18d));

        #endregion Fields

        #region Constructors

        public TeamView()
        {
            InitializeComponent();

            var s = IocFactory.Settings;
            ContentHeight = s.ContentHeight;
            ContentTextSize = s.ContentTextSize;
            ContentIconVisibility = s.ContentIconVisibility;
        }

        #endregion Constructors

        #region Properties

        public double ContentHeight
        {
            get => (double)GetValue(ContentHeightProperty);
            set => SetValue(ContentHeightProperty, value);
        }

        public bool ContentIconVisibility
        {
            get => (bool)GetValue(ContentIconVisibilityProperty);
            set => SetValue(ContentIconVisibilityProperty, value);
        }

        public double ContentTextSize
        {
            get => (double)GetValue(ContentTextSizeProperty);
            set => SetValue(ContentTextSizeProperty, value);
        }

        public IEnumerable<PersonDisplayModel> People
        {
            get => (IEnumerable<PersonDisplayModel>)GetValue(PeopleProperty);
            set => SetValue(PeopleProperty, value);
        }

        #endregion Properties
    }
}
