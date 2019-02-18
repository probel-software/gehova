using GalaSoft.MvvmLight;
using Probel.Gehova.Business.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Probel.Gehova.ViewModels.Vm.Settings
{
    public static class PersonFullDisplayViewModelExtensions
    {
        #region Methods

        public static PersonFullDisplayModel ToModel(this PersonFullDisplayViewModel src)
        {
            var result = new PersonFullDisplayModel
            {
                Id = src.Id,
                FirstName = src.FirstName,
                LastName = src.LastName,
                Category = src.CategoryDisplay,
                CategoryIds = (from c in src.Categories
                               where c.IsSelected
                               select c.Id).ToList(),
                ReceptionIds = (from r in src.Receptions
                                where r.IsSelected
                                select r.Id).ToList()
            };
            return result;
        }

        public static IEnumerable<PersonFullDisplayModel> ToModel(this IEnumerable<PersonFullDisplayViewModel> srcCollection)
        {
            var r = new List<PersonFullDisplayModel>();

            foreach (var item in srcCollection)
            {
                r.Add(item.ToModel());
            }
            return r;
        }

        public static PersonFullDisplayViewModel ToViewModel(this PersonFullDisplayModel src, IEnumerable<CategoryModel> categories = null)
        {
            var r = new PersonFullDisplayViewModel
            {
                Id = src.Id,
                FirstName = src.FirstName,
                LastName = src.LastName,
                CategoryDisplay = src.Category,
            };
            if (categories != null)
            {
                var c = categories.ToViewModel(src.CategoryKey);
                r.Categories = new ObservableCollection<CategoryViewModel>(c);
            }
            return r;
        }

        public static IEnumerable<PersonFullDisplayViewModel> ToViewModel(this IEnumerable<PersonFullDisplayModel> srcCollection, IEnumerable<CategoryModel> categories = null)
        {
            var r = new List<PersonFullDisplayViewModel>();
            foreach (var item in srcCollection)
            {
                r.Add(item.ToViewModel(categories));
            }
            return r;
        }

        #endregion Methods
    }

    public class PersonFullDisplayViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<CategoryViewModel> _categories = new ObservableCollection<CategoryViewModel>();
        private string _categoryDisplay;
        private string _firstName;
        private long _id;
        private string _lastName;

        private ObservableCollection<ReceptionViewModel> _receptions;

        #endregion Fields

        #region Properties

        public ObservableCollection<CategoryViewModel> Categories
        {
            get => _categories;
            set => Set(ref _categories, value, nameof(Categories));
        }

        public string CategoryDisplay
        {
            get => _categoryDisplay;
            set => Set(ref _categoryDisplay, value, nameof(CategoryDisplay));
        }

        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value, nameof(FirstName));
        }

        public long Id
        {
            get => _id;
            set => Set(ref _id, value, nameof(Id));
        }

        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value, nameof(LastName));
        }

        public ObservableCollection<ReceptionViewModel> Receptions
        {
            get => _receptions;
            set => Set(ref _receptions, value, nameof(Receptions));
        }

        #endregion Properties
    }
}