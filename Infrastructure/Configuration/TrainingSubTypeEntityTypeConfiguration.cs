using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal sealed class TrainingSubTypeEntityTypeConfiguration : IEntityTypeConfiguration<TrainingSubType>
{
    public void Configure(EntityTypeBuilder<TrainingSubType> builder)
    {
        builder.ToTable("TrainingSubTypes", TrainingDbContext.DefaultSchema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(x => x.TrainingType)
            .WithMany()
            .HasForeignKey(x => x.TrainingTypeId);

        builder.HasData(
            new TrainingSubType {Id = 1, TrainingTypeId = 1, Name = "Шоссе"},
            new TrainingSubType {Id = 2, TrainingTypeId = 1, Name = "Трейл"},
            new TrainingSubType {Id = 3, TrainingTypeId = 2, Name = "Шоссе"},
            new TrainingSubType {Id = 4, TrainingTypeId = 2, Name = "MTB"},
            new TrainingSubType {Id = 5, TrainingTypeId = 3, Name = "Классический стиль"},
            new TrainingSubType {Id = 6, TrainingTypeId = 3, Name = "Свободный стиль"},
            new TrainingSubType {Id = 7, TrainingTypeId = 3, Name = "Лыжероллеры"},
            new TrainingSubType {Id = 8, TrainingTypeId = 4, Name = "Бассейн"},
            new TrainingSubType {Id = 9, TrainingTypeId = 4, Name = "Открытая вода"});
    }
}