using Microsoft.Toolkit.Uwp.UI.Controls;
using Probel.Gehova.ViewModels.Infrastructure;

namespace Probel.Gehova.Views.Infrastructure
{
    public class InAppMessenger : IMessenger
    {
        #region Fields

        private const int DEFAULT_DURATION = 2000;
        private readonly InAppNotification _inAppNotification;

        #endregion Fields

        #region Constructors

        public InAppMessenger(InAppNotification inAppNotification)
        {
            _inAppNotification = inAppNotification;
        }

        #endregion Constructors

        #region Methods

        public void Say(string message) => _inAppNotification.Show(message, DEFAULT_DURATION);

        #endregion Methods
    }
}