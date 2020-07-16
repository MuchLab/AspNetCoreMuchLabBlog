using System.ComponentModel.DataAnnotations;

namespace MuchBlog.Api.Dtos
{
    public class EssayForCreationDto
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}