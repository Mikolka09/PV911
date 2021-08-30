using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebUserAjax.Entities;
using WebUserAjax.Entities.School;

namespace WebUserAjax.Data
{
    public class ApplicationDbContext : IdentityDbContext<ProjectUser>
    {
        public DbSet<Groupp> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
