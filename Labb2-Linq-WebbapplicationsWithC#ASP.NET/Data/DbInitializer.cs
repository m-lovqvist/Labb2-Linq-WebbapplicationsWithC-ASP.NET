using Labb2_Linq_WebbapplicationsWithC_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb2_Linq_WebbapplicationsWithC_ASP.NET.Data
{
    public class DbInitializer(ApplicationDbContext _context)
    {

        public async Task Initialize()
        {
            await _context.Database.EnsureCreatedAsync();
            if (_context.Students.Any() && _context.Courses.Any() && !_context.StudentCourses.Any())
            {
                await InitStudentCourses();
            }
            if (_context.Teachers.Any() && _context.Courses.Any() && !_context.TeacherCourses.Any())
            {
                await InitTeacherCourses();
            }

            if (_context.Students.Any())
            {
                return;
            }


            // -- Create and Initialize the jointable TeacherCourses and add to the DB -- //
            await InitTeachers();
            await InitClasses();
            await InitCourses();
            await InitStudents();
            await InitStudentCourses();
            await InitTeacherCourses();
        }

        private async Task InitTeacherCourses()
        {
            var teacherCourses = new List<TeacherCourse>
        {
            new() { TeacherId = 1, CourseId = 1 },   // Engelska 5
            new() { TeacherId = 1, CourseId = 11 },  // Svenska 2
            new() { TeacherId = 1, CourseId = 12 },  // Svenska 3
            new() { TeacherId = 2, CourseId = 15 },  // Kemi 1
            new() { TeacherId = 2, CourseId = 26 },  // Teknik 1
            new() { TeacherId = 1, CourseId = 10 },  // Svenska 1
            new() { TeacherId = 1, CourseId = 2 },   // Engelska 6
            new() { TeacherId = 3, CourseId = 23 },  //Psykolgi 1
            new() { TeacherId = 3, CourseId = 27 },  //Filosofi 1
            new() { TeacherId = 4, CourseId = 20 },  //Företagsekonomi 1
            new() { TeacherId = 4, CourseId = 21 },  //Privatjurudik 1
            new() { TeacherId = 5, CourseId = 4 },   //Idrott och hälsa 1
            new() { TeacherId = 5, CourseId = 22 },  //Moderna språk
            new() { TeacherId = 6, CourseId = 3 },   //Histroia 1b
            new() { TeacherId = 6, CourseId = 24 },  //Histroeia 1a1
            new() { TeacherId = 7, CourseId = 14 },  //Fysik 1
            new() { TeacherId = 7, CourseId = 25 },  //Fyik 1a1
            new() { TeacherId = 8, CourseId = 5 },   // Matematik 1c
            new() { TeacherId = 8, CourseId = 6 },   // Matematik 2c
            new() { TeacherId = 8, CourseId = 7 },   // Matematik 3c
            new() { TeacherId = 8, CourseId = 16 },  //Mateatik 1b
            new() { TeacherId = 8, CourseId = 17 },  //Matematik 2b
            new() { TeacherId = 9, CourseId = 13 },  //Biolodi 1
            new() { TeacherId = 8, CourseId = 18 },  //Naturkundskap 1b
            new() { TeacherId = 10, CourseId = 8 },  //Relegionskundskap 1
            new() { TeacherId = 10, CourseId = 9 },  //Samhällskundskapp 1b
            new() { TeacherId = 10, CourseId = 19 }, //Samhällskundskap 2

        };

            foreach (var teacherCourse in teacherCourses)
            {
                await _context.AddAsync(teacherCourse);
            }

            await _context.SaveChangesAsync();
        }

        private async Task InitStudentCourses()
        {
            var studentCourses = new List<StudentCourse>();

            var studentsInNa23 = _context.Students
        .Include(s => s.Class)
        .Where(c => c.Class.ClassName == "Na23").ToList();
            foreach (var student in studentsInNa23)
            {
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 1 });   // Engelska 5
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 2 });   // Engelska 6
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 3 });  // Historia 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 4 });   // Idrott och hälsa 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 5 });   // Matematik 1c
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 6 });   // Matematik 2c
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 7 });  // Matematik 3c
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 8 });   // Religionskunskap 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 9 });   // Samhällskunskap 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 10 });   // Svenska 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 11 });   // Svenska 2
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 12 });  // Svenska 3
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 13 });  // Biologi 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 14 });  // Fysik 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 15 });  // Kemi 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 22 });  // Moderna språk
            }
            var studentsInSa23 = _context.Students
                .Include(s => s.Class)
                .Where(c => c.Class.ClassName == "Sa23").ToList();

            foreach (var student in studentsInSa23)
            {
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 1 });   // Engelska 5
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 2 });   // Engelska 6
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 3 });  // Historia 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 4 });   // Idrott och hälsa 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 16 });  // Matematik 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 17 });  // Matematik 2b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 18 });  // Naturkunskap 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 8 });   // Religionskunskap 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 9 });   // Samhällskunskap 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 10 });   // Svenska 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 11 });   // Svenska 2
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 12 });  // Svenska 3
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 27 });  // Filosofi 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 22 });  // Moderna språk
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 23 });  // Psykologi 1
            }
            var studentsInEk23 = _context.Students
        .Include(s => s.Class)
        .Where(c => c.Class.ClassName == "Ek23").ToList();
            foreach (var student in studentsInEk23)
            {
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 1 });   // Engelska 5
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 2 });   // Engelska 6
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 3 });  // Historia 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 4 });   // Idrott och hälsa 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 16 });  // Matematik 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 17 });  // Matematik 2b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 18 });  // Naturkunskap 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 8 });   // Religionskunskap 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 9 });   // Samhällskunskap 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 19 });  // Samhällskunskap 2
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 10 });   // Svenska 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 11 });   // Svenska 2
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 12 });  // Svenska 3
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 20 });  // Företagsekonomi 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 21 });  // Privatjuridik
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 22 });  // Moderna språk
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 23 });  // Psykologi 1
            }
            var studentsInTe23 = _context.Students
        .Include(s => s.Class)
        .Where(c => c.Class.ClassName == "Te23").ToList();
            foreach (var student in studentsInTe23)
            {
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 1 });    // Engelska 5
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 2 });    // Engelska 6
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 24 });   // Historia 1a1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 4 });    // Idrott och hälsa 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 5 });   // Matematik 1c
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 6 });   // Matematik 2c
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 7 });   // Matematik 3c
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 8 });    // Religionskunskap 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 9 });    // Samhällskunskap 1b
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 10 });    // Svenska 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 11 });    // Svenska 2
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 12 });   // Svenska 3 / Svenska som andraspråk 3
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 25 });   // Fysik 1a
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 15 });   // Kemi 1
                studentCourses.Add(new() { StudentId = student.StudentId, CourseId = 26 });   // Teknik 1
            }


            foreach (var studentCourse in studentCourses)
            {
                await _context.AddAsync(studentCourse);
            }

            await _context.SaveChangesAsync();
        }



        private async Task InitCourses()
        {
            var courses = new List<Course>
        {
             new() { CourseName = "Engelska 5" },        //1
             new() { CourseName = "Engelska 6" },        //2
             new() { CourseName = "Historia 1b" },       //3
             new() { CourseName = "Idrott och hälsa 1" },//4
             new() { CourseName = "Matematik 1c" },      //5
             new() { CourseName = "Matematik 2c" },      //6
             new() { CourseName = "Matematik 3c" },      //7
             new() { CourseName = "Religionskunskap 1" },//8
             new() { CourseName = "Samhällskunskap 1b" },//9
             new() { CourseName = "Svenska 1" },         //10
             new() { CourseName = "Svenska 2" },         //11
             new() { CourseName = "Svenska 3" },         //12
             new() { CourseName = "Biologi 1" },         //13
             new() { CourseName = "Fysik 1" },           //14
             new() { CourseName = "Kemi 1" },            //15
             new() { CourseName = "Matematik 1b" },      //16
             new() { CourseName = "Matematik 2b" },      //17
             new() { CourseName = "Naturkunskap 1b" },   //18
             new() { CourseName = "Samhällskunskap 2" }, //19
             new() { CourseName = "Företagsekonomi 1" }, //20
             new() { CourseName = "Privatjuridik" },     //21
             new() { CourseName = "Moderna språk" },     //22
             new() { CourseName = "Psykologi 1" },       //23
             new() { CourseName = "Historia 1a1" },      //24
             new() { CourseName = "Fysik 1a" },          //25
             new() { CourseName = "Teknik 1" },          //26
             new() { CourseName = "Filosofi 1" }         //27
        };

            foreach (var course in courses)
            {
                await _context.AddAsync(course);
            }
            await _context.SaveChangesAsync();
        }

        private async Task InitClasses()
        {
            var classes = new List<Class>
        {
            new() { ClassName = "Na23" },
            new() { ClassName = "Sa23" },
            new() { ClassName = "Ek23" },
            new() { ClassName = "Te23" }
        };

            foreach (var c in classes)
            {
                await _context.AddAsync(c);
            }

            await _context.SaveChangesAsync();
        }

        private async Task InitStudents()
        {
            var students = new List<Student>
        {
            new () { StudentFirstName = "Alice", StudentLastName = "Andersson", ClassId = 1 },
            new () { StudentFirstName = "Bob", StudentLastName = "Berg", ClassId = 1 },
            new () { StudentFirstName = "Cecilia", StudentLastName = "Carlsson", ClassId = 1 },
            new () { StudentFirstName = "David", StudentLastName = "Dahl", ClassId = 1 },
            new () { StudentFirstName = "Eva", StudentLastName = "Eriksson", ClassId = 1 },
            new () { StudentFirstName = "Fredrik", StudentLastName = "Fransson", ClassId = 1 },
            new () { StudentFirstName = "Greta", StudentLastName = "Gustavsson", ClassId = 1 },
            new () { StudentFirstName = "Hans", StudentLastName = "Holm", ClassId = 1 },
            new () { StudentFirstName = "Ida", StudentLastName = "Isaksson", ClassId = 1 },
            new () { StudentFirstName = "Johan", StudentLastName = "Johansson", ClassId = 1 },
            new () { StudentFirstName = "Karin", StudentLastName = "Karlsson", ClassId = 1 },
            new () { StudentFirstName = "Lars", StudentLastName = "Lind", ClassId = 1 },
            new () { StudentFirstName = "Mona", StudentLastName = "Magnusson", ClassId = 1 },
            new () { StudentFirstName = "Nils", StudentLastName = "Norén", ClassId = 1 },
            new () { StudentFirstName = "Olivia", StudentLastName = "Olofsson", ClassId = 2 },
            new () { StudentFirstName = "Per", StudentLastName = "Persson", ClassId = 2 },
            new () { StudentFirstName = "Quentin", StudentLastName = "Qvist", ClassId = 2 },
            new () { StudentFirstName = "Rita", StudentLastName = "Rydén", ClassId = 2 },
            new () { StudentFirstName = "Stefan", StudentLastName = "Sundström", ClassId = 2 },
            new () { StudentFirstName = "Therese", StudentLastName = "Thor", ClassId = 2 },
            new () { StudentFirstName = "Ulf", StudentLastName = "Ulander", ClassId = 2 },
            new () { StudentFirstName = "Vera", StudentLastName = "Viklund", ClassId = 2 },
            new () { StudentFirstName = "William", StudentLastName = "Westerlund", ClassId = 2 },
            new () { StudentFirstName = "Xander", StudentLastName = "Xenon", ClassId = 2 },
            new () { StudentFirstName = "Ylva", StudentLastName = "Yngvesson", ClassId = 2 },
            new () { StudentFirstName = "Zacharias", StudentLastName = "Zetterlund", ClassId = 2 },
            new () { StudentFirstName = "Anna", StudentLastName = "Alm", ClassId = 3 },
            new () { StudentFirstName = "Bengt", StudentLastName = "Björk", ClassId = 3 },
            new () { StudentFirstName = "Clara", StudentLastName = "Claesson", ClassId = 3 },
            new () { StudentFirstName = "Daniel", StudentLastName = "Duvander", ClassId = 3 },
            new () { StudentFirstName = "Elin", StudentLastName = "Eklund", ClassId = 3 },
            new () { StudentFirstName = "Filip", StudentLastName = "Falk", ClassId = 3 },
            new () { StudentFirstName = "Gabriella", StudentLastName = "Gran", ClassId = 3 },
            new () { StudentFirstName = "Henry", StudentLastName = "Hansson", ClassId = 3 },
            new () { StudentFirstName = "Ingrid", StudentLastName = "Ingman", ClassId = 3 },
            new () { StudentFirstName = "Jakob", StudentLastName = "Jansson", ClassId = 3 },
            new () { StudentFirstName = "Katarina", StudentLastName = "Kullberg", ClassId = 3 },
            new () { StudentFirstName = "Leif", StudentLastName = "Lund", ClassId = 3 },
            new () { StudentFirstName = "Maria", StudentLastName = "Månsson", ClassId = 3 },
            new () { StudentFirstName = "Nina", StudentLastName = "Nilsson", ClassId = 4 },
            new () { StudentFirstName = "Oskar", StudentLastName = "Olsson", ClassId = 4 },
            new () { StudentFirstName = "Petra", StudentLastName = "Palm", ClassId = 4 },
            new () { StudentFirstName = "Qasim", StudentLastName = "Quist", ClassId = 4 },
            new () { StudentFirstName = "Rebecca", StudentLastName = "Rask", ClassId = 4 },
            new () { StudentFirstName = "Simon", StudentLastName = "Sjöberg", ClassId = 4 },
            new () { StudentFirstName = "Tina", StudentLastName = "Törnqvist", ClassId = 4 },
            new () { StudentFirstName = "Ursula", StudentLastName = "Udd", ClassId = 4 },
            new () { StudentFirstName = "Victor", StudentLastName = "Vikström", ClassId = 4 },
            new () { StudentFirstName = "Wilma", StudentLastName = "Wahl", ClassId = 4 },
            new () { StudentFirstName = "Xenia", StudentLastName = "Xandersson", ClassId = 4 }

        };

            foreach (var student in students)
            {
                await _context.AddAsync(student);
            }

            await _context.SaveChangesAsync();
        }

        private async Task InitTeachers()
        {
            var teachers = new List<Teacher>
        {
                    new() { TeacherFirstName = "Emma", TeacherLastName = "Andersson" },
                    new() { TeacherFirstName = "Johan", TeacherLastName = "Berg" },
                    new() { TeacherFirstName = "Sofia", TeacherLastName = "Carlsson" },
                    new() { TeacherFirstName = "Daniel", TeacherLastName = "Dahl" },
                    new() { TeacherFirstName = "Elin", TeacherLastName = "Eriksson" },
                    new() { TeacherFirstName = "Oscar", TeacherLastName = "Fredriksson" },
                    new() { TeacherFirstName = "Lina", TeacherLastName = "Gustavsson" },
                    new() { TeacherFirstName = "Nils", TeacherLastName = "Hansen" },
                    new() { TeacherFirstName = "Karin", TeacherLastName = "Isaksson" },
                    new() { TeacherFirstName = "Peter", TeacherLastName = "Johansson" }
       };

            foreach (var teacher in teachers)
            {
                await _context.AddAsync(teacher);
            }

            await _context.SaveChangesAsync();
        }
    }
}
