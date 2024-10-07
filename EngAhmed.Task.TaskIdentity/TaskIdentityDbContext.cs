using EngAhmed.TaskP.TaskIdentity.Extends;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EngAhmed.TaskP.TaskIdentity
{
    public class TaskIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public TaskIdentityDbContext(DbContextOptions<TaskIdentityDbContext> op) : base(op)
        {
            
        }

    }
}
