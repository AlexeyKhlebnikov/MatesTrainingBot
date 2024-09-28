using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal sealed class SubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions", TrainingDbContext.DefaultSchema);
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnType("bigserial")
            .ValueGeneratedNever();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("createdAt")
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.Radius)
            .HasColumnName("radius")
            .HasColumnType("integer");
        
        builder.Property(x => x.Location)
            .HasColumnName("location")
            .HasColumnType("geometry(Point,4326)");

        builder.HasOne(x => x.Subscriber)
            .WithMany()
            .IsRequired();

        builder.HasOne(x => x.Type)
            .WithMany()
            .IsRequired(false);

        builder.HasOne(x => x.SubType)
            .WithMany()
            .IsRequired(false);
    }
}