using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using Labb2_Linq_WebbapplicationsWithC_ASP.NET.Data;
using Labb2_Linq_WebbapplicationsWithC_ASP.NET.Models;


namespace Labb2_Linq_WebbapplicationsWithC_ASP.NET.Controllers
{
    public class TeacherCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeacherCourses.Include(t => t.Course).Include(t => t.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: TeacherCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFirstName");
            return View();
        }

        // POST: TeacherCourses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherId,CourseId")] TeacherCourse teacherCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", teacherCourse.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFirstName", teacherCourse.TeacherId);
            return View(teacherCourse);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int teacherId, int courseId)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherId == teacherId);
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (teacherId == 0 || courseId == 0)
            {
                return NotFound();
            }

            var teacherCourse = await _context.TeacherCourses
                .Include(tc => tc.Teacher)
                .Include(tc => tc.Course)
                .FirstOrDefaultAsync(tc => tc.CourseId == courseId && tc.TeacherId == teacherId);

            if (teacherCourse == null)
            {
                return NotFound();
            }

            SelectNewTeacherCourse viewModel = new()
            {
                Course = course,
                TeacherId = teacher.TeacherId,
                Teacher = teacher,
                NewTeacherList = new SelectList(_context.Teachers
                .Where(t => !_context.TeacherCourses.Any(tc => tc.CourseId == courseId && tc.TeacherId == t.TeacherId))
                .ToList(), "Id", "Name")
            };
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", teacherCourse.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFirstName", teacherCourse.TeacherId);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int courseId, int teacherId, int newTeacherId)
        {
            var existingTeacherCourse = _context.TeacherCourses
                    .Where(tc => tc.TeacherId == teacherId && tc.CourseId == courseId)
                    .FirstOrDefault();

            var newTeacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == newTeacherId);

            if (existingTeacherCourse == null || newTeacher == null)
            {
                return NotFound();
            }

            try
            {
                _context.TeacherCourses.Remove(existingTeacherCourse);
                await _context.SaveChangesAsync();

                TeacherCourse newTeacherCourse = new()
                {
                    TeacherId = newTeacherId,
                    CourseId = courseId
                };
                _context.TeacherCourses.Add(newTeacherCourse);
                await _context.SaveChangesAsync();
            }


            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherCourseExists(existingTeacherCourse.TeacherId, existingTeacherCourse.CourseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", "Courses");

        }



        // GET: TeacherCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherCourse = await _context.TeacherCourses
                .Include(t => t.Course)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeacherId == id);

            if (teacherCourse == null)
            {
                return NotFound();
            }

            return View(teacherCourse);
        }

        // POST: TeacherCourses/Delete/5
        [HttpPost("TeacherCourses/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherCourse = await _context.TeacherCourses.FindAsync(id);
            if (teacherCourse != null)
            {
                _context.TeacherCourses.Remove(teacherCourse);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherCourseExists(int teacherId, int courseId)
        {
            return _context.TeacherCourses.Any(tc => tc.TeacherId == teacherId && tc.CourseId == courseId);
        }

    }
}
