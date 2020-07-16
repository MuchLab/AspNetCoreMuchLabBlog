using System;
using System.ComponentModel.DataAnnotations;

namespace MuchBlog.Domain.Dtos
{
    public class ClassificationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}