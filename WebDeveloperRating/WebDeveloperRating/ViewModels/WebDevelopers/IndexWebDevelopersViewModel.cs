using WebDeveloperRating.ViewModels.Users;

namespace WebDeveloperRating.ViewModels.WebDevelopers
{
    public class IndexWebDevelopersViewModel : PagingViewModel
    {
        public IndexWebDevelopersViewModel() : base(10)
        {

        }
        public IndexWebDevelopersViewModel(int elementsCount, int itemsPerPage = 5, string action = "Index") : base(elementsCount, itemsPerPage, action)
        {
        }

        public string FilterByName { get; set; }

        public bool IsAsc { get; set; } = true;

        public ICollection<IndexWebDeveloperViewModel> WebDevelopers { get; set; } = new List<IndexWebDeveloperViewModel>();
    }
}
