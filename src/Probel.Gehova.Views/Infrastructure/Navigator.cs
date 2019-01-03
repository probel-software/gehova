using NLog;
using System;
using Windows.UI.Xaml.Controls;

namespace Probel.Gehova.Views.Infrastructure
{
    public class Navigator
    {
        #region Fields

        private static Navigator _instance = null;

        private static Frame _source;

        #endregion Fields

        #region Constructors

        private Navigator(ILogger logger)
        {
            _log = logger;
        }

        #endregion Constructors

        #region Properties

        public static Navigator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Navigator(LogManager.GetCurrentClassLogger());
                }
                return _instance;
            }
        }

        public ILogger _log { get; }

        #endregion Properties

        #region Methods

        public static Navigator GetInstance(Frame frame)
        {
            _source = frame;
            return Instance;
        }

        public void Navigate(Type destination)
        {
            if (_source != null)
            {
                _log.Trace($"Navigating to '{destination}'");
                _source.Navigate(destination);
            }
            else { throw new InvalidOperationException($"The source frame used for navigation is not set."); }
        }

        public void Navigate<TDestination>() => Navigate(typeof(TDestination));

        public void Navigate(Type destination, object context)
        {
            if (_source != null)
            {
                _log.Trace($"Navigating to '{destination}' with context '{context}'");
                _source.Navigate(destination, context);
            }
            else { throw new InvalidOperationException($"The source frame used for navigation is not set."); }
        }

        public void Navigate<TDestination>(object context) => Navigate(typeof(TDestination), context);

        #endregion Methods
    }
}