namespace AliveBlog.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<PostCategory>? PostCategories { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public ApplicationUser? Author { get; set; }
        public string? Slug { get; set; }
    }
}