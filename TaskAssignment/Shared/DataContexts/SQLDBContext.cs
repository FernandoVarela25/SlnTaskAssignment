using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssignment.Shared.Models;

namespace TaskAssignment.Shared.DataContexts
{
    public class SQLDBContext : DbContext
    {
        public SQLDBContext()
        {
        }
        public SQLDBContext(DbContextOptions<SQLDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Assignment> Assignments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:taskassignments.database.windows.net,1433;Initial Catalog=Assignments;Persist Security Info=False;User ID=veggetta;Password={Wiggetta7#};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Puedes configurar aquí las relaciones y propiedades de la tabla si es necesario
        }
    }
}
