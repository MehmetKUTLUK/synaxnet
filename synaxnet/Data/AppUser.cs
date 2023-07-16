using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace synaxnet.Data;

public class AppUser : IdentityUser<Guid>
{

    public required string Name { get; set; }
  
}

public class AppUserTypeConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder
            .HasIndex(x => x.Name)
            .IsUnique(false);
     
    }
}
