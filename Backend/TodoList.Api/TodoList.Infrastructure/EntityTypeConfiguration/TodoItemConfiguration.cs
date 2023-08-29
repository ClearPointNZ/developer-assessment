using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoList.Infrastructure;


internal class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("TodoItems");

        // Primary Key
        builder.HasKey(x => x.Id);

        builder.Property(t => t.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentitifer")
            .IsRequired();

        // Index
        builder.HasIndex(t => t.Id);
        builder.HasIndex(t => t.Description);


        // Fields
        builder.Property(t => t.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(t => t.IsCompleted)
            .HasColumnName("isCompleted");
    }
}
