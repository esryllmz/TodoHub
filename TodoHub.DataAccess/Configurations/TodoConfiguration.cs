using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Models.Entities;

namespace TodoHub.DataAccess.Configurations
{
  
    namespace TodoHub.DataAccess.Configurations
    {
        public class TodoConfiguration : IEntityTypeConfiguration<Todo>
        {

            public void Configure(EntityTypeBuilder<Todo> builder)
            {
                builder.HasKey(t => t.Id);

                builder.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(t => t.Description)
                    .HasMaxLength(500);

                builder.Property(t => t.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");

                builder.Property(t => t.Priority)
                    .HasConversion<int>(); // Priority enum'u int olarak saklanır

                builder.HasOne(t => t.Category) // Category ile ilişki
                    .WithMany(c => c.ToDos)
                    .HasForeignKey(t => t.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade); // Category silinirse ilgili Todo'lar da silinir

                builder.Property(t => t.Completed)
                    .HasDefaultValue(false);
            }
        }
    }

}
