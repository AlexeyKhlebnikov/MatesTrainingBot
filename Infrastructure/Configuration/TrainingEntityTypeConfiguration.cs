using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal sealed class TrainingEntityTypeConfiguration : IEntityTypeConfiguration<Training>
{
    public void Configure(EntityTypeBuilder<Training> builder)
    {
        builder.ToTable("Trainings", TrainingDbContext.DefaultSchema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnType("bigserial")
            .ValueGeneratedNever();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("createdAt")
            .IsRequired();

        builder.Property(x => x.Date)
            .HasColumnName("onDate")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.Time)
            .HasColumnName("onTime")
            .HasColumnType("time with time zone");

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.StartPoint)
            .HasColumnName("startPoint")
            .HasColumnType("geometry(Point,4326)")
            .IsRequired();

        builder.Property(x => x.AverageSpeed)
            .HasColumnName("averageSpeed")
            .HasColumnType("decimal")
            .IsRequired();

        builder.Property(x => x.Distance)
            .HasColumnName("distance")
            .HasColumnType("integer")
            .IsRequired();

        builder.HasOne(x => x.Author)
            .WithMany();

        builder.HasOne(x => x.TrainingType)
            .WithMany()
            .HasForeignKey(x=> x.TrainingTypeId);
    }
}