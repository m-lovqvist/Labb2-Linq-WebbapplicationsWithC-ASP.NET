using System.ComponentModel.DataAnnotations;

namespace Labb2_Linq_WebbapplicationsWithC_ASP.NET.Models
{
    public class Teacher
    {

        public int TeacherId { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        [Required(ErrorMessage = "Förnamn är obligatoriskt.")]
        [Display(Name = "Förnamn")]
        public string TeacherFirstName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        [Required(ErrorMessage = "Efternamn är obligatoriskt.")]
        [Display(Name = "Efternamn")]
        public string TeacherLastName { get; set; }

        public ICollection<TeacherCourse> TeacherCourses { get; set; }
    }
}
