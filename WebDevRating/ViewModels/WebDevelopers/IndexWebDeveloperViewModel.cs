using System.ComponentModel.DataAnnotations;

namespace WebDevRating.ViewsModels.Barbers
{
    public class IndexWebDeveloperViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Picture")]
        public string Image { get; set; }
    }
}
