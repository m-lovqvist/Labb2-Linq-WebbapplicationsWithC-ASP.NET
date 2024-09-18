using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2_Linq_WebbapplicationsWithC_ASP.NET.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name requires a minimum of 2 and a maximum of 50 characters")]
        [Display(Name = "Förnamn")]
        public string StudentFirstName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name requires a minimum of 2 and a maximum of 50 characters")]
        [Display(Name = "Efternamn")]
        public string StudentLastName { get; set; }


        public int ClassId { get; set; }
        public Class Class { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
