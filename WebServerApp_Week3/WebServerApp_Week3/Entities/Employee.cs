using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_2.Entities
{
    [Table("Employees")]
    public class Employee
    {
        public int EmployeeID { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(50)]
        public string maritalStatus { get; set; }

    }
}
