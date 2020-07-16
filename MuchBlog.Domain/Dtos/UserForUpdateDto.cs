using System;
using System.ComponentModel.DataAnnotations;

namespace MuchBlog.Api.Dtos
{
    public class UserForUpdateDto
    {
        [Required, MaxLength(20)]
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string BirthPlace { get; set; }
        [Required, MinLength(6), MaxLength(16)]
        public string Password { get; set; }
    }
}