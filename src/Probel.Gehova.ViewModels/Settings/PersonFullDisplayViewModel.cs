using GalaSoft.MvvmLight;
using Probel.Gehova.Business.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Probel.Gehova.ViewModels.Settings
{
    public static class PersonFullDisplayViewModelExtensions
    {
        #region Methods

        public static PersonFullDisplayModel ToModel(this PersonFullDisplayViewModel src)
        {
            var r = new PersonFullDisplayModel
            {
                Id = src.Id,
                FirstName = src.FirstName,
                LastName = src.LastName,
                Category = src.CategoryDisplay,
                IsReceptionMorning = src.IsReceptionMorning,
                IsLunchTime = src.IsLunchTime,
                IsReceptionEvening = src.IsReceptionEvening,
                CategoryIds = (from c in src.Categories
                               where c.IsSelected
                               select c.Id).ToList()
            };
            return r;
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
                IsReceptionMorning = src.IsReceptionMorning,
                IsLunchTime = src.IsLunchTime,
                IsReceptionEvening = src.IsReceptionEvening,
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
        private bool _isLunchTime;
        private bool _isReceptionEvening;
        private bool _isReceptionMorning;
        private string _lastName;

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

        public bool IsLunchTime
        {
            get => _isLunchTime;
            set => Set(ref _isLunchTime, value, nameof(_isLunchTime));
        }

        public bool IsReceptionEvening
        {
            get => _isReceptionEvening;
            set => Set(ref _isReceptionEvening, value, nameof(_isReceptionEvening));
        }

        public bool IsReceptionMorning
        {
            get => _isReceptionMorning;
            set => Set(ref _isReceptionMorning, value, nameof(IsReceptionMorning));
        }

        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value, nameof(LastName));
        }

        #endregion Properties
    }
}