using System;
using System.ComponentModel.DataAnnotations;

namespace MuchBlog.Infrastructure.Entities
{
    public class EssayImage:Entity
    {
        //外键
        public Guid EssayId { get; set; }
        public string FileName { get; set; }
        //导航属性
        public Essay Essay { get; set; }    
    }
}