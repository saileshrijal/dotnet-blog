using AliveBlog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AliveBlog.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Post>? Posts { get; set; }
    public DbSet<PostCategory>? PostCategories { get; set; }
}
