using System;

namespace MuchBlog.Api.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string BirthPlace { get; set; }
        public string Email { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string Password { get; set; }
    }
}