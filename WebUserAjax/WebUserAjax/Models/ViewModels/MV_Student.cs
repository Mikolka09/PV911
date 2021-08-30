using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebUserAjax.Entities.School;

namespace WebUserAjax.Models.ViewModels
{
    public class MV_Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Groupp Groupp { get; set; }
        public string GrouppName { get; set; }
        public Teacher Teacher { get; set; }

        public MV_Student(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            Groupp = student.Groupp;
            GrouppName = student.Groupp.Name;
            Teacher = student.Groupp.Teacher;
        }

        public MV_Student FromModel (Student student)
        {
            Id = student.Id;
            Name = student.Name;
            Groupp = student.Groupp;
            GrouppName = student.Groupp.Name;
            Teacher = student.Groupp.Teacher;
            return this;
        }
    }
}
