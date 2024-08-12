using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using domain;
using domain.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext<myapp>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
