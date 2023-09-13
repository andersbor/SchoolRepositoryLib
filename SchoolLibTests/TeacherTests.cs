using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLib.Tests
{
    [TestClass()]
    public class TeacherTests
    {
        [TestMethod()]
        public void ValidateNameTest()
        {
            Teacher anders = new Teacher() { Id = 1, Name = "A", Salary = 0 };
            anders.ValidateName();

            Teacher nullName = new Teacher() { Id = 1, Name = null, Salary = 0 };
            Assert.ThrowsException<ArgumentNullException>(
                () => nullName.ValidateName());

        }

        [TestMethod()]
        public void ValidateSalaryTest()
        {

            Teacher anders = new Teacher() { Id = 1, Name = "Anders", Salary = 0 };
            // anders.ValidateSalary();

            Teacher negativeSalary = new Teacher() { Id = 1, Name = "Anders", Salary = -1 };
            // Assert.ThrowsException<ArgumentOutOfRangeException>(
            //     () => negativeSalary.ValidateSalary());


        }
    }
}