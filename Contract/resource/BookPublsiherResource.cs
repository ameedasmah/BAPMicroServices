using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.Resourse
{
    public class BookPublisherResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Discraptions { get; set; }
        public int? PublisherId { get; set; }
        public PublisherBookResource Newpublisher { get; set; }
        public List<BookAuthorResource> BookAuthorResources { get; set; }
    }
}