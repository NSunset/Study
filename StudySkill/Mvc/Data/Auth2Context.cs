using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Data
{
    public class Auth2Context : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {

        public Auth2Context(DbContextOptions<Auth2Context> options):base(options)
        {

        }


        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<ApplicationRole> ApplicationRole { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            builder.Entity<ApplicationRole>().ToTable("ApplicationRole");
        }
    }
}
