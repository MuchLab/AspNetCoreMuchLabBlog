using System;
using System.ComponentModel.DataAnnotations;

namespace MuchBlog.Infrastructure.Entities
{
    public abstract class Entity
    {
        [Required]
        public Guid Id { get; set; }
    }
}