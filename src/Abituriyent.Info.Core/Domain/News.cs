using System;

namespace Abituriyent.Info.Core.Domain
{
    public class News : Entity
    {
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }

        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}