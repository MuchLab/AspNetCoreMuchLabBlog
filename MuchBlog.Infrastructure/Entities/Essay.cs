using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuchBlog.Infrastructure.Entities
{
    public class Essay:Entity
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTimeOffset CreateDate { get; set; }
        [Required]
        public DateTimeOffset LastModified { get; set; }

        //public double Star { get; set; }

        //外键
        public Guid UserId { get; set; }
        //导航属性
        public User User { get; set; }
        //导航属性
        public List<EssayClassification> EssayClassifications { get; set; }
        //导航属性
        public List<EssayImage> EssayImages { get; set; }
    }
}