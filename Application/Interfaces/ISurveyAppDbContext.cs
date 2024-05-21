using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISurveyAppDbContext
    {
        DbSet<Domain.Models.Survey> Surveys { get; set; }  
        DbSet <Option> Options { get; set; }
        DbSet<Vote> Votes { get; set; }

        Task <int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
