using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2_Linq_WebbapplicationsWithC_ASP.NET.Models
{
    public class TeacherCourse
    {

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }


        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
