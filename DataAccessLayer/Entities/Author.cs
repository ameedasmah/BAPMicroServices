using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.Entities
{
    public class Author
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Pole wymagane")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Email Address cannot have white spaces")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Range(1,100, ErrorMessage = "Age must be between 1 to 100 years.")]
        public int Age { get; set; }
        //Navigations properties
        public ICollection<Book>? Books { get; set; }

        //public List<Book_Author> book_Authors { get; set; }
    }
}