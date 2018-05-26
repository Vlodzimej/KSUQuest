using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSUQuest.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
