using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.Entities
{
    public class StudentModel
    {
        public int StudentID { get; set;  }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string Address { get; set; }
        public bool IsActive {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ? UpdatedAt { get; set; }

        }
}
