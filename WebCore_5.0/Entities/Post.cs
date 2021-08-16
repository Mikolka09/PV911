using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore_5._0.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }

        //Установка отношений 1 to many
       
        public Category Category { get; set; } //1 to many

        //Установка отношений многие ко многим
        //public ICollection<Tag> Tags { get; set; }
    }
}
