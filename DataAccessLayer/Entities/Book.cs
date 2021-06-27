using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        [Required]

        public string Title { get; set; }
        [Required(ErrorMessage = "It's not allowed to be null")]
        [MaxLength(500)]
        public string Discraptions { get; set; }
        [Required(ErrorMessage = "It's not allowed to be null")]
        //Navigation Properites
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public ICollection<Author> Authors { get; set; }



    }
}
