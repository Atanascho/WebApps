using WebDeveloperRating.Data;
using WebDeveloperRating.Data.Models;
using WebDeveloperRating.Services.Contracts;
using WebDeveloperRating.ViewModels.WebDevelopers;
using Microsoft.EntityFrameworkCore;

namespace WebDeveloperRating.Services
{
    public class WebDevelopersService : IWebDevelopersService
    {
        private readonly ApplicationDbContext context;

        public WebDevelopersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> UpdateWebDeveloperAsync(EditWebDeveloperViewModel model)
        {
            WebDeveloper? WebDeveloper = await context
                .WebDevelopers
                .FindAsync(model.Id);

            WebDeveloper.Name = model.Name;
            WebDeveloper.Description = model.Description;

            context.WebDevelopers.Update(WebDeveloper);
            await context.SaveChangesAsync();
            return WebDeveloper.Id;
        }

        public async Task<EditWebDeveloperViewModel> GetWebDeveloperToEditAsync(string WebDeveloperId)
        {
            WebDeveloper? WebDeveloper = await context
                .WebDevelopers
                .FindAsync(WebDeveloperId);

            return new EditWebDeveloperViewModel()
            {
                Id = WebDeveloperId,
                Name = WebDeveloper.Name,
                Description = WebDeveloper.Description,
            };
        }

        public async Task<string> CreateWebDeveloperAsync(CreateWebDeveloperViewModel model)
        {
            WebDeveloper WebDeveloper = new WebDeveloper()
            {
                Name = model.Name,
                Description = model.Description,
                Image = await ImageToStringAsync(model.ImageFile),
            };
            await context.WebDevelopers.AddAsync(WebDeveloper);
            await context.SaveChangesAsync();

            return WebDeveloper.Id;
        }

        public async Task<IndexWebDevelopersViewModel> GetWebDevelopersAsync(IndexWebDevelopersViewModel model)
        {
            if (model == null)
            {
                model = new IndexWebDevelopersViewModel(10);
            }
            IQueryable<WebDeveloper> dataWebDevelopers = context.WebDevelopers;

            if (!string.IsNullOrWhiteSpace(model.FilterByName))
            {
                dataWebDevelopers = dataWebDevelopers.Where(x => x.Name.Contains(model.FilterByName));
            }

            if (model.IsAsc)
            {
                model.IsAsc = false;
                dataWebDevelopers = dataWebDevelopers.OrderByDescending(x => x.Name);
            }
            else
            {
                model.IsAsc = true;
                dataWebDevelopers = dataWebDevelopers.OrderBy(x => x.Name);
            }

            model.ElementsCount = await dataWebDevelopers.CountAsync();

            model.WebDevelopers = await dataWebDevelopers
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .Select(x => new IndexWebDeveloperViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                })
                .ToListAsync();

            return model;
        }



        private async Task<string> ImageToStringAsync(IFormFile file)
        {
            List<string> imageExtensions = new List<string>() { ".JPG", ".BMP", ".PNG" };


            if (file != null) // check if the user uploded something
            {
                var extension = Path.GetExtension(file.FileName); //get file extension
                if (imageExtensions.Contains(extension.ToUpperInvariant()))
                {
                    using var dataStream = new MemoryStream();
                    await file.CopyToAsync(dataStream);
                    byte[] imageBytes = dataStream.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
            return null;
        }
    }
}
