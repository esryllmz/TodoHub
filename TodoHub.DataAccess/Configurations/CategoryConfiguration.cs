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
                builder.ToTable("Categories").HasKey(c => c.Id);
                builder.Property(c => c.Id).HasColumnName("CategoryId");
                builder.Property(c => c.CreatedTime).HasColumnName("CreatedTime");
                builder.Property(c => c.UpdatedTime).HasColumnName("UpdatedTime");
                builder.Property(c => c.Name).HasColumnName("CategoryName");

                builder
                  .HasMany(c => c.Todos)
                  .WithOne(t => t.Category)
                  .HasForeignKey(c => c.CategoryId)
                  .OnDelete(DeleteBehavior.NoAction);

                builder.HasData(new Category()
                {
                    Id = 1,
                    Name = "Yemek",
                    CreatedTime = DateTime.Now
                });

            }



        }
    }


}
