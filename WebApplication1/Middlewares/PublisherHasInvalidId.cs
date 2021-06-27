using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorPublisherProject.Middlewares
{
    public class PublisherHasInvalidId: Exception 
    {
        public PublisherHasInvalidId(string message) : base(message)
        {

        }
    }
}
