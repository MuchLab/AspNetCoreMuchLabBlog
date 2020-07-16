using System.ComponentModel.DataAnnotations;

namespace MuchBlog.Api.Dtos
{
    public class EssayForUpdateDto
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}