using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCrudAPI.Models
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> projects { get; set; }
        public DbSet<Group> groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Group>().HasData(
                new Group() { GroupId = 1, GroupName = "Group1"},
                new Group() { GroupId = 2, GroupName = "Group2" },
                new Group() { GroupId = 3, GroupName = "Group3" }
                );
        }
    }
}
