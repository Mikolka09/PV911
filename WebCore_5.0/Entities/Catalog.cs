using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore_5._0.Entities
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //самосвязанная таблица
        public int? ParentId { get; set; }
        public Catalog Parent { get; set; }
    }
}
