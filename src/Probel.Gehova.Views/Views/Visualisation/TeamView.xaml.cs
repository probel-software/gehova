using Probel.Gehova.Business.Models;
using System.Collections.Generic;
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

        #endregion Fields

        #region Constructors

        public TeamView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public IEnumerable<PersonDisplayModel> People
        {
            get => (IEnumerable<PersonDisplayModel>)GetValue(PeopleProperty);
            set => SetValue(PeopleProperty, value);
        }

        #endregion Properties
    }
}