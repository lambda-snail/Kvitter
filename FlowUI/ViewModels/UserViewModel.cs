using System;
using System.ComponentModel.DataAnnotations;

namespace FlowUI.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "The name is too long, please use less than 64 characters.")]
        public string FirstName { get; set; }

        [StringLength(64, ErrorMessage = "The name is too long, please use less than 64 characters.")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("(?:1\\d\\d)|(?:[1-9]\\d)|(?:[1-9])", ErrorMessage = "Age must be greater than 0 and less than 199.")]
        public string Age { get; set; } // Form cannot map to int

        [Required]
        [StringLength(64, ErrorMessage = "The short description must be less than 64 characters long.")]
        public string ShortDescription { get; set; }

        [StringLength(2048, ErrorMessage = "The description can be at most 2048 characters long.")]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset SignUpTime { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}
