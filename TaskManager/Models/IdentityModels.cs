using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public string FistName { get; set; }                
        
        [StringLength(255)]
        public string Address { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<SubTasksLevel1> SubTasksLevel1 { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<MemberGroups> MemberGroups { get; set; }
        public DbSet<MemberPositions> MemberPosition { get; set; }
        public DbSet<TaskTypes> TaskTypes { get; set; }
        public DbSet<TaskCategories> TaskCategories { get; set; }
        public DbSet<TaskStatuses> TaskStatuses { get; set; }
        public DbSet<TaskProcedures> TaskProcedures { get; set; }
        public DbSet<Prices> Prices { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<TaskPool> TaskPool { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}