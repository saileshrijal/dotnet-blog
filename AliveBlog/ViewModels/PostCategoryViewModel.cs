using AliveBlog.Models;

namespace AliveBlog.ViewModels
{
    public class PostCategoryViewModel
    {
        public List<PostCategory>? PostCategories { get; set; }
        public string? CategoryName { get; set; }
    }
}