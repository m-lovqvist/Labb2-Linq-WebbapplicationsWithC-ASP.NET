using System.ComponentModel.DataAnnotations;

namespace Labb2_Linq_WebbapplicationsWithC_ASP.NET.Models
{
    public class Class
    {
        public int ClassId { get; set; }

        [StringLength(10, MinimumLength = 2, ErrorMessage = "Name requires a minimum of 2 and a maximum of 10 characters")]
        [Display(Name = "Klass")]
        public string ClassName { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
