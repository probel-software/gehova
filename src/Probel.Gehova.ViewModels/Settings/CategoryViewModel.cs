using GalaSoft.MvvmLight;
using Probel.Gehova.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.ViewModels.Settings
{
    public static class CategoryViewModelExtesions
    {
        #region Methods

        public static IEnumerable<CategoryModel> ToModel(this IEnumerable<CategoryViewModel> srcCollection)
        {
            var r = new List<CategoryModel>();
            foreach (var item in srcCollection)
            {
                r.Add(item.ToModel());
            }
            return r;
        }

        public static CategoryModel ToModel(this CategoryViewModel src)
        {
            var r = new CategoryModel
            {
                Display = src.Name,
                Id = src.Id,
            };
            return r;
        }

        public static IEnumerable<CategoryViewModel> ToViewModel(this IEnumerable<CategoryModel> srcCollection, string categoryKeys = null)
        {
            var r = new List<CategoryViewModel>();

            foreach (var item in srcCollection)
            {
                r.Add(item.ToViewModel(categoryKeys));
            }
            return r;
        }

        public static CategoryViewModel ToViewModel(this CategoryModel src, string categoryKeys = null)
        {
            var r = new CategoryViewModel
            {
                IsSelected = false,
                Name = src.Display,
                Id = src.Id,
            };

            if (!string.IsNullOrEmpty(categoryKeys))
            {
                var keys = categoryKeys.Replace(" ", "").Split(',');
                r.IsSelected = keys.Contains(src.Key);
            }
            return r;
        }

        #endregion Methods
    }

    public class CategoryViewModel : ViewModelBase
    {
        #region Fields

        private long _id;
        private bool _isSelected;

        private string _name;

        #endregion Fields

        #region Properties

        public long Id
        {
            get => _id;
            set => Set(ref _id, value, nameof(Id));
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value, nameof(IsSelected));
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value, nameof(Name));
        }
        public override string ToString() => $"[{(IsSelected? "x" : " ")}] {Name}";
        #endregion Properties
    }
}