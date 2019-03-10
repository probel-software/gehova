using System.Windows.Input;

namespace Probel.Gehova.Views.Helpers
{
    public static class CommandExtension
    {
        #region Methods

        public static bool TryExecute(this ICommand cmd, object parameter = null)
        {
            if (cmd == null) { return false; }
            if (cmd.CanExecute(parameter))
            {
                cmd.Execute(parameter);
                return true;
            }
            else { return false; }
        }

        #endregion Methods
    }
}