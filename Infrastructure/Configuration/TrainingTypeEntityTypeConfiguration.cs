using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal sealed class TrainingTypeEntityTypeConfiguration : IEntityTypeConfiguration<TrainingType>
{
    public void Configure(EntityTypeBuilder<TrainingType> builder)
    {
        builder.ToTable("TrainingTypes", TrainingDbContext.DefaultSchema);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseSerialColumn();
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasData(new TrainingType {Id = 1, Name = "Бег"},
            new TrainingType {Id = 2, Name = "Велоспорт"},
            new TrainingType {Id = 3, Name = "Беговые лыжи"},
            new TrainingType {Id = 4, Name = "Плавание"});
    }
}