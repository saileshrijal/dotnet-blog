using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AliveBlog.ViewModels
{
    public class PostViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ApplicationUserId { get; set; }
        public string? Slug { get; set; }

        [Display(Name = "Publish")]
        public bool IsPublished { get; set; }

        [Display(Name = "Featured Photo")]
        public IFormFile? FeaturedPhoto { get; set; }

        [Display(Name = "Published On")]
        public DateTime PublishedOn { get; set; } = DateTime.Now;
        public List<SelectListItem>? Categories { get; set; }

        [Required]
        public string? Excerpt { get; set; }

        [Display(Name = "Banner")]
        public bool IsBanner { get; set; }
    }
}