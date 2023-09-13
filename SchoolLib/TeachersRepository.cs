using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLib
{
    public class TeachersRepository
    {
        private int nextId = 1;
        private List<Teacher> teachers = new();

        public TeachersRepository()
        {
            teachers.Add(new Teacher { Id = nextId++, Name = "John", Salary = 1000 });
            teachers.Add(new Teacher { Id = nextId++, Name = "Mary", Salary = 2000 });
            teachers.Add(new Teacher { Id = nextId++, Name = "Peter", Salary = 3000 });
        }

        public List<Teacher> Get()
        {
            return new List<Teacher> (teachers);
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
    }
}
