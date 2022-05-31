using System.ComponentModel.DataAnnotations;

namespace AliveBlog.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<PostCategory>? PostCategories { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public string? AuthorId { get; set; }
        public ApplicationUser? Author { get; set; }
        public string? Slug { get; set; }
        public bool IsPublished { get; set; }
        public DateTime PublishedOn { get; set; } = DateTime.Now;

        [Required]
        public string? Excerpt { get; set; }

        [Display(Name = "Banner")]
        public bool IsBanner { get; set; }
    }
}