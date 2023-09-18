using Microsoft.EntityFrameworkCore;
// NuGet Microsoft.EntityFrameworkCore
using Microsoft.Extensions.Options;
// NuGet Microsoft.EntityFrameworkCore.SqlServer
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesRepositoryLib;
using SchoolLib;

namespace SchoolLib.Tests
{
    [TestClass()]
    public class TeachersRepositoryTests
    {
        private static ITeachersRepository teachersRepository;
        private static bool useDatabase = true;

        [ClassInitialize]
        public static void InitOnce(TestContext context)
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<SchoolDBContext>();
                optionsBuilder.UseSqlServer(Secrets.ConnectionStringSimply);
                // connection string structure
                //   "Data Source=mssql7.unoeuro.com;Initial Catalog=FROM simply.com;Persist Security Info=True;User ID=FROM simply.com;Password=DB PASSWORD FROM simply.com;TrustServerCertificate=True"
                SchoolDBContext _dbContext = new(optionsBuilder.Options);
                // clean database table: remove all rows
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Teachers");
                teachersRepository = new TeacherRepositoryDB(_dbContext);
            }
            else
            {
                teachersRepository = new TeachersRepositoryList();
            }

        }

        // Test methods are execute in alphabetical order

        [TestMethod]
        public void AddGetTest()
        {
            IEnumerable<Teacher> teachers = teachersRepository.Get(namePart: "an");
            Assert.AreEqual(0, teachers.Count());
            Teacher teacher = new() { Name = "Jan", Salary = 1000 };
            teachersRepository.Add(teacher);
            IEnumerable<Teacher> teachers2 = teachersRepository.Get();
            Assert.AreEqual(teachers.Count() + 1, teachers2.Count());
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Teacher? teacher = teachersRepository.GetById(1);
            Assert.IsNotNull(teacher);
            Assert.AreEqual(1, teacher.Id);

            Teacher? teacher2 = teachersRepository.GetById(100);
            Assert.IsNull(teacher2);
        }

        [TestMethod]
        public void RemoveTest()
        {
            Teacher? teacher = teachersRepository.Remove(1);
            Assert.IsNotNull(teacher);
            Assert.AreEqual(1, teacher.Id);
            Teacher? teacher2 = teachersRepository.Remove(1);
            Assert.IsNull(teacher2);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Teacher teacher = new() { Name = "Jan", Salary = 1000 };
            teachersRepository.Add(teacher);

            Teacher data = new() { Name = "Jan", Salary = 2000 };
            Teacher? updated = teachersRepository.Update(teacher.Id, data);
            Assert.IsNotNull(updated);
            // Assert.AreEqual(2000, teacher.Salary);
            Teacher? found = teachersRepository.GetById(teacher.Id);
            Assert.IsNotNull(found);
            Assert.AreEqual(2000, found.Salary);

            Teacher? updated2 = teachersRepository.Update(100, data);
            Assert.IsNull(updated2);

            teachersRepository.Remove(updated.Id); // clean up
        }

        [TestMethod]
        public void XGetParametersTest()
        {
            teachersRepository.Add(new Teacher { Name = "John", Salary = 1000 });
            teachersRepository.Add(new Teacher { Name = "Mary", Salary = 2000 });
            teachersRepository.Add(new Teacher { Name = "Peter", Salary = 3000 });
            teachersRepository.Add(new Teacher { Name = "Jan A", Salary = 100 });
            teachersRepository.Add(new Teacher { Name = "Jan B", Salary = 2000 });
           
            List<Teacher> teachers = teachersRepository.Get(orderBy: "salary").ToList();
            Assert.AreEqual(100, teachers[0].Salary);
           
            teachers = teachersRepository.Get(orderBy: "salary_desc").ToList();
            Assert.AreEqual(3000, teachers[0].Salary);

            teachers = teachersRepository.Get(namePart: "a").ToList();
            Assert.AreEqual(3, teachers.Count);

            teachers = teachersRepository.Get(salaryBelow: 1000).ToList();      
            Assert.AreEqual(1, teachers.Count);

            teachers = teachersRepository.Get(namePart: "a", salaryBelow: 1000).ToList();   
            Assert.AreEqual(1, teachers.Count); 
        }
    }
}