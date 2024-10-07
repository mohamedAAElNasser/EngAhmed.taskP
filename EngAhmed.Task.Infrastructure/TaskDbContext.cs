using EngAhmed.TaskP.Domain.Enitities.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Infrastructure
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions op) : base(op)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }


        #region Override ChangeTracker
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
            }



            return base.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
