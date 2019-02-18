using Probel.Gehova.Business.Models;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Probel.Gehova.Views.Views.Visualisation
{
    public sealed partial class ReceptionGroupView : PivotItem
    {
        #region Fields

        public static DependencyProperty ReceptionsProperty = DependencyProperty.Register(
                "Reception",
                typeof(ReceptionModel),
                typeof(ReceptionGroupView),
                null
            );

        #endregion Fields

        #region Constructors

        public ReceptionGroupView()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public ReceptionModel Reception
        {
            get => (ReceptionModel)GetValue(ReceptionsProperty);
            set => SetValue(ReceptionsProperty, value);
        }

        #endregion Properties
    }
}