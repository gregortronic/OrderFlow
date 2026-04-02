using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Documents;

namespace OrderFlow.Infrastructure.Persistence.Configurations;

public sealed class DocumentJobConfiguration : IEntityTypeConfiguration<DocumentJob>
{
    public void Configure(EntityTypeBuilder<DocumentJob> builder)
    {
        builder.ToTable("document_jobs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.OriginalFileName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.StoredFileName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.ContentType)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.ErrorMessage)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CreatedAtUtc);
    }
}