using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLib
{
    public class TeacherRepositoryDB : ITeachersRepository
    {
        private readonly SchoolDBContext context;

        public TeacherRepositoryDB(SchoolDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<Teacher> Get(string? orderBy = null, string? namePart = null, int? salaryBelow = null)
        {
            IEnumerable<Teacher> query = context.Teachers.ToList().AsQueryable();
            // copy, ToList()
            if (namePart != null)
            {
                query = query.Where(teacher =>
                               teacher.Name != null && teacher.Name.Contains(namePart));
            }
            if (salaryBelow != null)
            {
                query = query.Where(teacher => teacher.Salary < salaryBelow);
            }
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "name":
                    case "name_asc":
                        query = query.OrderBy(t => t.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(t => t.Name);
                        break;
                    case "salary":
                    case "salary_asc":
                        query = query.OrderBy(t => t.Salary);
                        break;
                    case "salary_desc":
                        query = query.OrderByDescending(t => t.Salary);
                        break;
                    default:
                        break; // do nothing
                        //throw new ArgumentException("Unknown sort order: " + orderBy);
                }
            }

            return query;
        }

        public Teacher? GetById(int id)
        {
            return context.Teachers.FirstOrDefault(t => t.Id == id);
        }

        public Teacher Add(Teacher teacher)
        {
            teacher.Validate();
            teacher.Id = 0;
            context.Teachers.Add(teacher);
            context.SaveChanges();
            return teacher;
        }

        public Teacher? Remove(int id)
        {
            Teacher? teacher = context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null) return null;
            context.Teachers.Remove(teacher);
            context.SaveChanges();
            return teacher;
        }

        public Teacher? Update(int id, Teacher data)
        {
            data.Validate();
            Teacher? teacher = context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null) return null;
            teacher.Name = data.Name;
            teacher.Salary = data.Salary;
            context.SaveChanges();
            return teacher;
        }
    }
}
