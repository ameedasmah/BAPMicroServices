using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.models
{
    public class PublisherModel
    {
        [Required(ErrorMessage = "The field is required")]
        [StringLength(25, ErrorMessage = "The field must be less than {25} characters", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pole wymagane")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Email Address cannot have white spaces")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "y")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Range(2000, 5000, ErrorMessage = "it must be between 2000 and 5000 $")]
        public float Salery { get; set; }
    }
}
