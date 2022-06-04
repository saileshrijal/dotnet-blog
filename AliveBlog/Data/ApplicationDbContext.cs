using AliveBlog.Models;
using Microsoft.AspNetCore.Identity;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var UserID = "02f5abee-e4d8-45c1-88f5-6121e124bba9";
        var AdminRoleID = "da45c571-6e39-4a2b-905c-d4e2d01292b6";
        var AuthorRoleID = "b876f2c1-a9b6-4bc0-9c6b-8675c15c94ea";

        var applicationUser = new ApplicationUser
        {
            Id = UserID,
            UserName = "superadmin@gmail.com",
            NormalizedUserName = "SUPERADMIN@GMAIL.COM",
            Email = "superadmin@gmail.com",
            NormalizedEmail = "SUPERADMIN@GMAIL.COM",
            EmailConfirmed = true,
            FirstName = "Super",
            LastName = "Admin"
        };

        PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
        applicationUser.PasswordHash = ph.HashPassword(applicationUser, "admin");

        builder.Entity<ApplicationUser>().HasData(applicationUser);

        builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
        {
            new IdentityRole {
                Id = "da45c571-6e39-4a2b-905c-d4e2d01292b6",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole {
                Id = "b876f2c1-a9b6-4bc0-9c6b-8675c15c94ea",
                Name = "Author",
                NormalizedName = "AUTHOR"
            },
        });


        builder.Entity<IdentityUserRole<string>>().HasData(
        new IdentityUserRole<string>
        {
            RoleId = "da45c571-6e39-4a2b-905c-d4e2d01292b6", // for admin username
            UserId = "02f5abee-e4d8-45c1-88f5-6121e124bba9" // for admin role
        });
        base.OnModelCreating(builder);
    }
}
