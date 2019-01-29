using GalaSoft.MvvmLight;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Probel.Gehova.ViewModels.Infrastructure
{
    public class AsyncViewModel : ViewModelBase
    {
        protected void ExecuteAsync<T>(Func<T> onThread, Action<T> onGui = null)
        {
            var token = new CancellationTokenSource().Token;
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task.Run(onThread);
            if (onGui != null)
            {
                task.ContinueWith(t => onGui(t.Result), token
                                                  , TaskContinuationOptions.OnlyOnRanToCompletion
                                                  , context);
            }
            //task.ContinueWith(t => ErrorService.Handle(t.Exception, Messages.Msg_ErrorOccured)
            //                                  , token
            //                                  , TaskContinuationOptions.OnlyOnFaulted
            //                                  , context);
        }
    }
}
