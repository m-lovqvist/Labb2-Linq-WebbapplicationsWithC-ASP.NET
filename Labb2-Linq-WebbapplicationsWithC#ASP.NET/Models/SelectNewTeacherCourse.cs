using Microsoft.AspNetCore.Mvc.Rendering;

namespace Labb2_Linq_WebbapplicationsWithC_ASP.NET.Models
{
    public class SelectNewTeacherCourse
    {
        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        public int? NewTeacherId { get; set; }
        public Teacher? NewTeacher { get; set; }
        public SelectList? NewTeacherList { get; set; }
    }
}
