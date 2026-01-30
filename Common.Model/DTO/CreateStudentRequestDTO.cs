using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Common.Model.DTO
{
   public  class CreateStudentRequestDTO
    {   
        [Required]
        [StringLength(50, MinimumLength =3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 10)]
        public string MobileNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly  DateOfBirth { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FathersName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string MothersName { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 5)]
        public string Address { get; set; }
    }
}
