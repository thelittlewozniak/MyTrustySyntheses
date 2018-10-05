using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedCode;

namespace WebApi.Models
{
    public class Context:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<File> Files  { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public Context(DbContextOptions<Context> options)
           : base(options)
        { }
    }
}
