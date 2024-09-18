using System.ComponentModel.DataAnnotations;

namespace Labb2_Linq_WebbapplicationsWithC_ASP.NET.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name requires a minimum of 3 and a maximum of 20 characters")]
        [Display(Name = "Kurs")]
        public string CourseName { get; set; }

        public ICollection<TeacherCourse> TeacherCourses { get; set; } = [];
        public ICollection<StudentCourse> StudentCourses { get; set; } = [];
    }
}
