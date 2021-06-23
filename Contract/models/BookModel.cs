using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.models
{
    public class BookModel
    {
        [Required(ErrorMessage = "It's not allowed to be null")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "It's not allowed to be null")]
        [MaxLength(500)]
        public String Discraptions { get; set; }
        [Required(ErrorMessage = "It's not allowed to be null")]
        public int PublisherId { get; set; }
        //[Required(ErrorMessage = "It's not allowed to be null")]
        public List<int> AuthorIds { get; set; }
    }
}

