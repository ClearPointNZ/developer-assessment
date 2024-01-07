using DeveloperAssessment.Modules.Todos.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperAssessment.Modules.Todos.Core.DAL.EF.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.IsCompleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
    }
}
