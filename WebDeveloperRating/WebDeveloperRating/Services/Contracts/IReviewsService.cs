using WebDeveloperRating.ViewModels.Reviews;

namespace WebDeveloperRating.Services.Contracts
{
    public interface IReviewsService
    {
        public Task<string> CreateReviewAsync(CreateReviewViewModel model, string userId);

        public Task<IndexReviewsUserViewModel> GetUserReviewsAsync(IndexReviewsUserViewModel model, string userId);
    }
}
