using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contract.Resourse
{
    public class PublisherResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public float Salery { get; set; }
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "y")]
        public DateTime DateOfBirth { get; set; }
        public List<PublisherBookCreateResource> Books { get; set; }
    }
}
