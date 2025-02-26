using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

internal class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext(options)
{

}
