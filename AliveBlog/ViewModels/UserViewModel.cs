using System.ComponentModel.DataAnnotations;

namespace AliveBlog.ViewModels
{
    public class UserViewModel
    {
        public string? UserId { get; set; }

        [Required, Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "About Me")]
        public string? AboutMe { get; set; }

        [Required]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        public string? ReturnUrl { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile? ProfilePicture { get; set; }

        public string? ProfilePictureUrl { get; set; }
    }
}