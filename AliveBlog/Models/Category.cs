namespace AliveBlog.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public List<PostCategory>? PostCategories { get; set; }
    }
}