using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Probel.Gehova.Views.Helpers
{
    public static class DependencyObjectExtensions
    {
        #region Methods

        public static DependencyObject GetAncestorByType<TType>(this DependencyObject src, int depth = 0)
        {
            var type = typeof(TType);

            if (depth == 30) { return null; }
            else if (src == null) { return null; }
            else if (src.GetType() == type) { return src; }
            else { return GetAncestorByType<TType>(src, ++depth); }
        }

        public static T GetParentOfType<T>(this DependencyObject element) where T : DependencyObject
        {
            var type = typeof(T);
            if (element == null) { return null; }

            var parent = VisualTreeHelper.GetParent(element);
            if (parent == null && ((FrameworkElement)element).Parent is DependencyObject) { parent = ((FrameworkElement)element).Parent; }
            if (parent == null) { return null; }
            else if (parent.GetType() == type || parent.GetType().IsSubclassOf(type)) { return parent as T; }

            return GetParentOfType<T>(parent);
        }

        #endregion Methods
    }
}