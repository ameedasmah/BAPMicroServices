using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string  Name { get; set; }
        [Required(ErrorMessage = "Pole wymagane")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Email Address cannot have white spaces")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Range(2000,5000,ErrorMessage="it must be between 2000 and 5000 $")]
        public float Salery { get; set; }
        //Navigation Properites

        public List<Book> Books { get; set; }
    }
}
