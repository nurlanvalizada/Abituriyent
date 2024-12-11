using System;

namespace Abituriyent.Info.Core.Domain
{
    public class ContactMessage : Entity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
    }
}