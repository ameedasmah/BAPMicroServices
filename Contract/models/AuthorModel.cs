using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.models
{
    public class AuthorModel
    {
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
        [Range(1, 100, ErrorMessage = "Age must be between 1 to 100 years.")]
        public int Age { get; set; }

    }
}
