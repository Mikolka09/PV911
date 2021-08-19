using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore_5._0.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        //many to many
        public ICollection<Product> Products { get; set; }

        //public ICollection<Post> Posts { get; set; }
    }
}
