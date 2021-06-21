using System;
using System.Collections.Generic;

namespace Contract.Resourse
{
    public class PublisherResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Salery { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<PublisherBookCreate> Books { get; set; }
    }
}
