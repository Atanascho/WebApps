using WebDeveloperRating.Data;
using WebDeveloperRating.Data.Models;
using WebDeveloperRating.Services.Contracts;
using WebDeveloperRating.ViewModels.WebDevelopers;
using WebDeveloperRating.ViewModels.Reviews;
using Microsoft.EntityFrameworkCore;

namespace WebDeveloperRating.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly ApplicationDbContext context;

        public ReviewsService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<string> CreateReviewAsync(CreateReviewViewModel model, string userId)
        {
            Review review = new Review()
            {
                Rating = model.Rating,
                UserId = userId,
                ReviewText = model.ReviewText,
                WebDeveloperId = model.WebDeveloperId
            };

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();

            return review.Id;
        }

        public async Task<IndexReviewsUserViewModel> GetUserReviewsAsync(IndexReviewsUserViewModel model, string userId)
        {
            if (model == null)
            {
                model = new IndexReviewsUserViewModel(10);
            }
            IQueryable<Review> reviewsData = context.Reviews.Where(x => x.UserId == userId);

            model.ElementsCount = await reviewsData.CountAsync();

            model.UserReviews = await reviewsData
              .Skip((model.Page - 1) * model.ItemsPerPage)
              .Take(model.ItemsPerPage)
              .Select(x => new IndexReviewViewModel()
              {
                  ReviewId=x.Id,
                  WebDeveloperName = x.WebDeveloper.Name,
                  UserId = userId,
                  Rating=x.Rating,
                  ReviewText=x.ReviewText
              })
              .ToListAsync();

            return model;
        }
    }
}
