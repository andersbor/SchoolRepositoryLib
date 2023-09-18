using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLib
{
    public class TeachersRepositoryList : ITeachersRepository
    {
        private int nextId = 1;
        private readonly List<Teacher> teachers = new();

        public TeachersRepositoryList()
        {
            //teachers.Add(new Teacher { Id = nextId++, Name = "John", Salary = 1000 });
            //teachers.Add(new Teacher { Id = nextId++, Name = "Mary", Salary = 2000 });
            //teachers.Add(new Teacher { Id = nextId++, Name = "Peter", Salary = 3000 });
        }

        public IEnumerable<Teacher> Get(string? orderBy = null, string? namePart = null, int? salaryBelow = null)
        {
            IEnumerable<Teacher> result = new List<Teacher>(teachers);
            if (namePart != null)
            {
                result = result.Where(teacher =>
                teacher.Name != null && teacher.Name.Contains(namePart));
            }
            if (salaryBelow != null)
            {
                result = result.Where(
                    teacher => teacher.Salary < salaryBelow);
            }
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                result = orderBy switch
                {
                    "name" => result.OrderBy(teacher => teacher.Name),
                    "name_asc" => result.OrderBy(teacher => teacher.Name),
                    "name_desc" => result.OrderByDescending(teacher => teacher.Name),
                    "salary" => result.OrderBy(teacher => teacher.Salary),
                    "salary_asc" => result.OrderBy(teacher => teacher.Salary),
                    "salary_desc" => result.OrderByDescending(teacher => teacher.Salary),
                    _ => throw new ArgumentException("Invalid orderBy value")
                };
            }
            return result.ToList();
        }

        public Teacher Add(Teacher teacher)
        {
            teacher.Validate();
            teacher.Id = nextId++;
            teachers.Add(teacher);
            return teacher;
        }

        public Teacher? GetById(int id)
        {
            return teachers.FirstOrDefault(t => t.Id == id);
        }

        public Teacher? Remove(int id)
        {
            Teacher? teacher = GetById(id);
            if (teacher != null)
            {
                teachers.Remove(teacher);
            }
            return teacher;
        }

        public Teacher? Update(int id, Teacher data)
        {
            Teacher? teacher = GetById(id);
            if (teacher == null) return null;
            teacher.Name = data.Name;
            teacher.Salary = data.Salary;
            return teacher;
        }
    }
}
