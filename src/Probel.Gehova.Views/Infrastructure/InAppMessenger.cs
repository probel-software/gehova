using Microsoft.Toolkit.Uwp.UI.Controls;
using Probel.Gehova.ViewModels.Infrastructure;

namespace Probel.Gehova.Views.Infrastructure
{
    public class InAppMessenger : IUserMessenger
    {
        #region Fields

        public const int DEFAULT_DURATION = 5000;
        private readonly InAppNotification _inAppNotification;

        #endregion Fields

        #region Constructors

        public InAppMessenger()
        {
            _inAppNotification = MainPage.Messenger;
        }

        #endregion Constructors

        #region Methods

        public void Say(string message) => _inAppNotification.Show(message, DEFAULT_DURATION);

        #endregion Methods
    }
}