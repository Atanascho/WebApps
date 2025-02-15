using WebDeveloperRating.ViewModels.WebDevelopers;

namespace WebDeveloperRating.Services.Contracts
{
    public interface IWebDevelopersService
    {
        public Task<string> CreateWebDeveloperAsync(CreateWebDeveloperViewModel model);

        public Task<IndexWebDevelopersViewModel> GetWebDevelopersAsync(IndexWebDevelopersViewModel model);

        public Task<string> UpdateWebDeveloperAsync(EditWebDeveloperViewModel model);

        public Task<EditWebDeveloperViewModel> GetWebDeveloperToEditAsync(string WebDeveloperId);
    }
}
