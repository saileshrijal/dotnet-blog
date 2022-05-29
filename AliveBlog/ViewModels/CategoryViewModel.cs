using System.ComponentModel.DataAnnotations;

namespace AliveBlog.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}