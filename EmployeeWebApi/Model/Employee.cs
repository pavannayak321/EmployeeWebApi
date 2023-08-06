using System.ComponentModel.DataAnnotations;

namespace EmployeeWebApi.Model
{
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
