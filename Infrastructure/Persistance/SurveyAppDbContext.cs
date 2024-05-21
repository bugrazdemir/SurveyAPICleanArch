using Application.Interfaces;
using Domain.Models;
using Infrastructure.Persistance.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Persistance;

public class SurveyAppDbContext : DbContext, ISurveyAppDbContext 
{
    private readonly IConfiguration _configuration;

        public SurveyAppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var comnectionString = _configuration.GetConnectionString("DefaultConnection");
        base.OnConfiguring(optionsBuilder);
        var builder=new NpgsqlDataSourceBuilder(comnectionString);
        builder.EnableDynamicJson();
        var dataSource=builder.Build();
        optionsBuilder.UseNpgsql(dataSource);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SurveyConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
