using System;

namespace MuchBlog.Infrastructure.Entities
{
    public class EssayClassification
    {
        //外键
        public Guid EssayId { get; set; }
        //外键
        public Guid ClassificationId { get; set; }
        //导航属性，和Essay是一对多的关系
        public Essay Essay { get; set; }
        //导航属性，和Classification是一对多的关系
        public Classification Classification { get; set; }
    }
}