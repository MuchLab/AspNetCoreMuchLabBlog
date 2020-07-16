using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuchBlog.Infrastructure.Entities
{
    public class Classification:Entity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } 
        //导航属性
        public List<EssayClassification> EssayClassifications { get; set; }
    }
}