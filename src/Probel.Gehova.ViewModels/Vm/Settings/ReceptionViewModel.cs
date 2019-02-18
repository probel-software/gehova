using GalaSoft.MvvmLight;
using Probel.Gehova.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.ViewModels.Vm.Settings
{
    public static class ReceptionViewModelExtensions
    {
        #region Methods

        public static IEnumerable<ReceptionModel> ToModel(this IEnumerable<ReceptionViewModel> srcCollection)
        {
            var r = new List<ReceptionModel>();
            foreach (var item in srcCollection)
            {
                r.Add(item.ToModel());
            }
            return r;
        }

        public static ReceptionModel ToModel(this ReceptionViewModel src)
        {
            var r = new ReceptionModel
            {
                ReceptionName = src.Name,
                Id = src.Id,
            };
            return r;
        }

        public static IEnumerable<ReceptionViewModel> ToViewModel(this IEnumerable<ReceptionModel> srcCollection, string categoryKeys = null)
        {
            var r = new List<ReceptionViewModel>();

            foreach (var item in srcCollection)
            {
                r.Add(item.ToViewModel(categoryKeys));
            }
            return r;
        }

        public static ReceptionViewModel ToViewModel(this ReceptionModel src, string categoryKeys = null)
        {
            var r = new ReceptionViewModel
            {
                IsSelected = false,
                Name = src.ReceptionName,
                Id = src.Id,
            };

            if (!string.IsNullOrEmpty(categoryKeys))
            {
                var keys = categoryKeys.Replace(" ", "").Split(',');
                r.IsSelected = keys.Contains(src.Id.ToString());
            }
            return r;
        }

        #endregion Methods
    }
    public class ReceptionViewModel : ViewModelBase
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

        #endregion Properties

        #region Methods

        public override string ToString() => $"[{(IsSelected ? "x" : " ")}] {Name}";

        #endregion Methods
    }
}