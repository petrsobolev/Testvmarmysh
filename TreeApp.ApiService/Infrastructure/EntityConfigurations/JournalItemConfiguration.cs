using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreeApp.ApiService.Model;

namespace TreeApp.ApiService.Infrastructure.EntityConfigurations;

public class JournalItemConfiguration : IEntityTypeConfiguration<JournalItem>
{
    public void Configure(EntityTypeBuilder<JournalItem> builder)
    {
        builder.ToTable("ExceptionRecords");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EventId).IsRequired();
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Timestamp).IsRequired();
        builder.Property(x => x.Message).IsRequired();
        builder.Property(x => x.StackTrace).IsRequired();
        builder.Property(x => x.QueryParametrs).IsRequired();
        builder.Property(x => x.BodyParametrs).IsRequired();
        builder.HasIndex(x => x.Timestamp);
    }
}