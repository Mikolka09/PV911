using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore_5._0.Entities;

namespace WebCore_5._0.Models
{
    public class MyUser: IdentityUser
    {
        public int Year { get; set; }

        //public ICollection<Post> Posts { get; set; }
    }
}
