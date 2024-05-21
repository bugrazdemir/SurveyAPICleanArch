using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistance.Configuration;

public class VoteConfiguration: IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder) 
    {
        builder.ToTable("vote");
        builder.HasKey(x => x.Id);
        builder.Property(x=> x.Id).HasColumnName("id").HasColumnType("int");
        builder.Property(x=>x.User).HasColumnName("user").HasColumnType("varchar(50)");

        builder.HasOne(x => x.Survey).WithMany(x => x.Votes);
        builder.HasMany(x=>x.Options).WithMany(x => x.Votes);
    }
}
