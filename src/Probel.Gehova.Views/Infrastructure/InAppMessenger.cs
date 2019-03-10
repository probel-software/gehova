using Microsoft.Toolkit.Uwp.UI.Controls;
using Probel.Gehova.ViewModels.Infrastructure;

namespace Probel.Gehova.Views.Infrastructure
{
    public class InAppMessenger : IUserMessenger
    {
        #region Fields

        private readonly InAppNotification _inAppNotification;
        public const int DEFAULT_DURATION = 5000;

        #endregion Fields

        #region Constructors

        public InAppMessenger()
        {
            _inAppNotification = MainPage.Messenger;
        }

        #endregion Constructors

        #region Methods

        public void Say(string message)
        {
            _inAppNotification.DataContext = InAppMessage.Info(message);
            _inAppNotification.Show(DEFAULT_DURATION);
        }

        public void Warn(string message)
        {
            _inAppNotification.DataContext = InAppMessage.Warning(message);
            _inAppNotification.Show(DEFAULT_DURATION);
        }

        #endregion Methods

        #region Classes

        private class InAppMessage
        {
            #region Constructors

            private InAppMessage(string message, string glyph)
            {
                Message = message;
                Glyph = glyph;
            }

            #endregion Constructors

            #region Properties

            public string Glyph { get; set; }

            public string Message { get; private set; }


            #endregion Properties

            #region Methods

            public static InAppMessage Info(string message) => new InAppMessage(message, "\uE946");

            public static InAppMessage Warning(string message) => new InAppMessage(message, "\uEA39");

            #endregion Methods
        }

        #endregion Classes
    }
}