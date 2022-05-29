using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AliveBlog.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Remote(action: "VerifyCategoryName", areaName: "Admin", controller: "Category", AdditionalFields = "InitialTitle")]
        public string? Title { get; set; }
        public string? InitialTitle { get; set; }
        public string? Description { get; set; }
    }
}