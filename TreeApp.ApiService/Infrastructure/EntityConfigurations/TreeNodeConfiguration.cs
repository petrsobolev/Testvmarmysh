using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreeApp.ApiService.Model;

namespace TreeApp.ApiService.Infrastructure.EntityConfigurations;

public class TreeNodeConfiguration: IEntityTypeConfiguration<TreeNode>
{
    public void Configure(EntityTypeBuilder<TreeNode> builder)
    {
        builder.ToTable("TreeNodes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Root)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.Children)
            .WithOne(x => x.Parent)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.Name);
    }
}