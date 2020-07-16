using System;
using System.Collections.Generic;

namespace MuchBlog.Api.Dtos
{
    public class EssayDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string Author { get; set; }
        public string[] ClassificationList { get; set; }
    }
}