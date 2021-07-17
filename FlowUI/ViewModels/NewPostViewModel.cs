using System;
using System.ComponentModel.DataAnnotations;

namespace FlowUI.ViewModels
{
    public class NewPostViewModel
    {
        public Guid PostOwnerId { get; set; }
        
        public DateTimeOffset PostedDateTime { get; set; }
        
        [Required]
        [StringLength(32, ErrorMessage = "Title cannot be longer than 32 characters.")]
        public string Title { get; set; }
        [Required]
        [StringLength(1024, ErrorMessage = "Please write less than 1024 characters.")]
        public string Content { get; set; }
    }
}
