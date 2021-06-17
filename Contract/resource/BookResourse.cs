
using System.Collections.Generic;

namespace Contract.Resourse
{
    public class BookResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string discraptions { get; set; }
        public int PublisherId { get; set; }
        public PublisherBookResource publisher { get; set; }
        public List<BookAuthorResource> AuthoursNameList { get; set; }

    }
}
