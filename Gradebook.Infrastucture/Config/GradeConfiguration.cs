using Gradebook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gradebook.Infrastructure.Config;

public class GradeConfiguration : BaseEntityConfiguration<Grade>
{
    public override void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable("Grades");

        builder.HasIndex(x => new { x.Name, x.Value })
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasPrecision(2, 1)
            .IsRequired();

        builder.HasMany(x => x.Students)
            .WithOne(x => x.Grade)
            .HasForeignKey(x => x.GradeId);

        base.Configure(builder);
    }
}
