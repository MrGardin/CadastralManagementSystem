using CadastralManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CadastralManagement.Data
{
    public class CadastralManagementContext(DbContextOptions<CadastralManagementContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public required DbSet<CadastralObject> CadastralObjects { get; set; }
    }
}
