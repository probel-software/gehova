using GalaSoft.MvvmLight;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Probel.Gehova.ViewModels.Infrastructure
{
    public class AsyncViewModel : ViewModelBase
    {
        #region Fields

        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        #endregion Fields

        #region Methods

        protected void ExecuteAsync<T>(Func<T> onThread, Action<T> onGui = null)
        {
            var token = new CancellationTokenSource().Token;
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task.Run(onThread);
            if (onGui != null)
            {
                task.ContinueWith(t => onGui(t.Result), token
                        , TaskContinuationOptions.OnlyOnRanToCompletion
                        , context)
                    .ContinueWith(t =>
                    {
                        if (t.Status == TaskStatus.Faulted)
                        {
                            Logger.Error(t.Exception, "An error occured (in the BLL) while executing work on a thread");
                        }
                    });
            }
            task.ContinueWith(t => Logger.Error(t.Exception, "An error occured (in the GUI) while executing work on a thread"), token
                , TaskContinuationOptions.OnlyOnFaulted
                , context);
        }

        #endregion Methods
    }
}