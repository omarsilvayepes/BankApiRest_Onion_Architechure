using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser> // for register and login process
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
    }
}
