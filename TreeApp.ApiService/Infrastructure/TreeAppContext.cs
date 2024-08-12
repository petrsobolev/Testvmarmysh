using Microsoft.EntityFrameworkCore;
using TreeApp.ApiService.Infrastructure.Converters;
using TreeApp.ApiService.Infrastructure.EntityConfigurations;
using TreeApp.ApiService.Model;

namespace TreeApp.ApiService.Infrastructure;

public class TreeAppContext(DbContextOptions<TreeAppContext> options) : DbContext(options)
{
    public const string SchemaName = "trees";

    public DbSet<TreeNode?> TreeNodes { get; init; }
    public DbSet<JournalItem> JournalItems { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaName);
        modelBuilder.ApplyConfiguration(new TreeNodeConfiguration());
        modelBuilder.ApplyConfiguration(new JournalItemConfiguration());
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetValueConverter>();
    }
}