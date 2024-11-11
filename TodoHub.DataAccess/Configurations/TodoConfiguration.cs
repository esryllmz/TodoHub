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
                builder.ToTable("Todos").HasKey(t => t.Id);
                builder.Property(t => t.Id).HasColumnName("TodoId");
                builder.Property(t => t.CreatedTime).HasColumnName("CreatedTime");
                builder.Property(t => t.UpdatedTime).HasColumnName("UpdatedTime");
                builder.Property(t => t.Title).HasColumnName("Title");
                builder.Property(t => t.Description).HasColumnName("Description");
                builder.Property(t => t.StartDate).HasColumnName("StartDate");
                builder.Property(t => t.EndDate).HasColumnName("EndDate");
                builder.Property(t => t.Priority).HasColumnName("Priority");
                builder.Property(t => t.CategoryId).HasColumnName("Category_Id");
                builder.Property(t => t.UserId).HasColumnName("User_Id");

                builder
                  .HasOne(t => t.Category)
                  .WithMany(c => c.Todos)
                  .HasForeignKey(t => t.CategoryId)
                  .OnDelete(DeleteBehavior.NoAction);
            }
        }
    }

}
