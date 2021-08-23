using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore_5._0.Entities.School
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GrouppId { get; set; }
        public Groupp Groupp { get; set; }
    }
}
