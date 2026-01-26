using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPulse.Domain.Entities;

namespace ProjectPulse.Infrastructure.Data.Configurations
{
    public class ProjectUserConfiguration : IEntityTypeConfiguration<ProjectUser>
    {
        public void Configure(EntityTypeBuilder<ProjectUser> builder)
        {
            builder.ToTable("ProjectUsers");

            builder.HasKey(u=>u.Id);

            builder.Property(pu => pu.CreatedAt)
                .IsRequired();

            builder.Property(pu => pu.UpdatedAt)
                .IsRequired(false);

            // Relationships
            builder.HasOne(pu => pu.Project)
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(pu => pu.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pu => pu.User)
                .WithMany(u => u.ProjectUsers)
                .HasForeignKey(pu => pu.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite index to prevent duplicate assignments
            builder.HasIndex(pu => new { pu.ProjectId, pu.UserId })
                .IsUnique();
        }
    }

}