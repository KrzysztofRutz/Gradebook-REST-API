using Gradebook.Domain.Entities;
using Gradebook.Infrastructure.Config.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gradebook.Infrastructure.Config;

public class StudentConfiguration : BaseEntityConfiguration<Student>
{
    public override void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.Property(s => s.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.LastName)
           .HasMaxLength(50)
           .IsRequired();

        builder.HasIndex(x => x.Email)
           .IsUnique();
        builder.Property(s => s.Email)
          .HasMaxLength(100)
          .IsRequired();

        builder.Property(s => s.DateOfBirth)
            .HasConversion<DateOnlyConverter>()
            .HasColumnType("date")
            .IsRequired();

        builder.Property(s => s.YearEnrolled)
            .IsRequired();

        builder.HasOne(x => x.Grade)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.GradeId);

        builder.HasOne(x => x.Address)
            .WithOne(x => x.Student)
            .HasForeignKey<Address>(x => x.StudentId);

        base.Configure(builder);
    }
}
