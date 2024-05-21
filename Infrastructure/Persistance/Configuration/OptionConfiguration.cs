using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration;

public class OptionConfiguration:IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder) 
    {
        builder.ToTable("option");
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Id).HasColumnName("id").HasColumnType("int");
        builder.Property(x=>x.Description).HasColumnName("description").HasColumnType("varchar(255)");
        builder.Property(x=>x.Order).HasColumnName("order").HasColumnType("int");
        builder.Property(x => x.Type).HasColumnName("type").HasColumnType("varchar(50)").HasDefaultValue("checkbox");

        builder.HasOne(x => x.Survey).WithMany(x => x.Options);

    }
}
