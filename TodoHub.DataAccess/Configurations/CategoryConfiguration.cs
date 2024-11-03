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
        public class CategoryConfiguration : IEntityTypeConfiguration<Category>
        {
           

            public void Configure(EntityTypeBuilder<Category> builder)
            {
                builder.HasKey(c => c.Id);

                builder.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.HasMany(c => c.ToDos) // Category'den Todo'lara birden çok ilişki
                    .WithOne(t => t.Category)
                    .HasForeignKey(t => t.CategoryId);
            }
        }
    }

}
