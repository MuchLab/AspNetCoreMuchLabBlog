using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuchBlog.Infrastructure.Entities
{
    public class User:Entity
    {
        [Required, MaxLength(20)]
        public string UserName { get; set; }
        [Required, MinLength(6), MaxLength(16)]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string BirthPlace { get; set; }
        //导航属性
        public List<Essay> Essays { get; set; }
    }
}