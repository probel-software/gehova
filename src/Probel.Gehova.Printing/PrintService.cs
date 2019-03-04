using Microsoft.Toolkit.Uwp.Helpers;
using Probel.Gehova.ViewModels.Infrastructure;
using System.Threading.Tasks;
using Windows.Graphics.Printing;
using Windows.UI.Xaml.Controls;

namespace Probel.Gehova.Printing
{
    public class PrintService
    {
        #region Fields

        private readonly IUserMessenger _messenger;
        private readonly Panel _panel;
        private PrintHelper _printHelper;

        #endregion Fields

        #region Constructors

        public PrintService(Panel panel, IUserMessenger messenger)
        {
            _messenger = messenger;
            _panel = panel;
        }

        #endregion Constructors

        #region Methods

        private void OnPrintFailed()
        {
            _printHelper?.Dispose();
            _messenger.Say($"Printing miserably failed...");
        }

        private void OnPrintSucceeded()
        {
            _printHelper?.Dispose();
            _messenger.Say($"Printing proudly succeed...");
        }

        public async Task Print(Page page, string title)
        {
            _printHelper = new PrintHelper(_panel);
            _printHelper.OnPrintSucceeded += OnPrintSucceeded;
            _printHelper.OnPrintFailed += OnPrintFailed;

            _printHelper.AddFrameworkElementToPrint(page);
            var opt = new PrintHelperOptions(false) { Orientation = PrintOrientation.Landscape };
            await _printHelper.ShowPrintUIAsync(title, opt);
        }

        #endregion Methods
    }
}