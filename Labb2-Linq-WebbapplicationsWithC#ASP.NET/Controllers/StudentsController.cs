using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb2_Linq_WebbapplicationsWithC_ASP.NET.Data;
using Labb2_Linq_WebbapplicationsWithC_ASP.NET.Models;


namespace Labb2_Linq_WebbapplicationsWithC_ASP.NET.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(int? classId)
        {
            var students = from s in _context.Students
                           select s;

            if (classId.HasValue)
            {
                students = students.Where(s => s.ClassId == classId.Value);
            }

            var classes = await _context.Classes.ToListAsync();
            ViewBag.Classes = classes;
            ViewBag.SelectedClassId = classId;

            return View(await students.Include(s => s.Class).Include(s => s.StudentCourses).ThenInclude(sc => sc.Course).ThenInclude(c => c.TeacherCourses).ThenInclude(tc => tc.Teacher).ToListAsync());
        }


        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentFirstName,StudentLastName,ClassId,TeacherId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName", student.ClassId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync();


            if (student == null)
            {
                return NotFound();
            }

            ViewData["id"] = new SelectList(_context.Classes, "ClassId", "ClassName", student.ClassId);

            return View(student);
        }


        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {

            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudentId))
                {
                    ModelState.AddModelError(string.Empty, "Model.State is not valid!");
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }

            return RedirectToAction(nameof(Index));


        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
