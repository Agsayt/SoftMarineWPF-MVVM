using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftMarineWPF_MVVM.Core.Models;
using System.Net.Mail;

namespace SoftMarineWPF_MVVM.Core.Context;
public class CoreContext: DbContext
{
    public CoreContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Inspection>(InspectionConfiguration);
        builder.Entity<Inspector>(InspectorConfiguration);
        builder.Entity<Remark>(RemarkConfiguration);

        base.OnModelCreating(builder);
    }

    private void InspectionConfiguration(EntityTypeBuilder<Inspection> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Inspector).WithMany(x => x.Inspections);
    }

    private void InspectorConfiguration(EntityTypeBuilder<Inspector> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Inspections).WithOne(x => x.Inspector);

        builder.HasIndex(x => x.UniqueNumber).IsUnique();
    }

    private void RemarkConfiguration(EntityTypeBuilder<Remark> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Inspection).WithMany(x => x.Remarks);
    }

}
