using Probel.Gehova.Views.Infrastructure;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Visualisation
{
    public sealed partial class PersonView : UserControl
    {
        #region Fields

        public static DependencyProperty ContentTextSizeProperty = DependencyProperty.Register(
            "ContentTextSize",
            typeof(double),
            typeof(PersonView),
            new PropertyMetadata(16d));

        public static DependencyProperty ContentIconVisibilityProperty = DependencyProperty.Register(
            "ContentIconVisibility",
            typeof(Visibility),
            typeof(PersonView),
            new PropertyMetadata(Visibility.Visible));

        #endregion Fields

        #region Constructors

        public PersonView()
        {
            InitializeComponent();

            var s = IocFactory.Settings;
            ContentTextSize = s.ContentTextSize;
            ContentIconVisibility = new GridLength(s.ContentIconVisibility ? 35 : 0);
        }

        #endregion Constructors

        #region Properties

        public double ContentTextSize
        {
            get => (double)GetValue(ContentTextSizeProperty);
            set => SetValue(ContentTextSizeProperty, value);
        }

        public GridLength ContentIconVisibility
        {
            get => (GridLength)GetValue(ContentIconVisibilityProperty);
            set => SetValue(ContentIconVisibilityProperty, value);
        }

        #endregion Properties
    }
}
