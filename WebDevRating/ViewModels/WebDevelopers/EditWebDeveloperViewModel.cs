using System.ComponentModel.DataAnnotations;

namespace WebDevRating.ViewsModels.Barbers
{
    public class EditWebDeveloperViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
