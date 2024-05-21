using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Survey.Queries;

 public class GetSurveyQuery:IRequest<List<Domain.Models.Survey>>
{
    public GetSurveyQuery(int surveyId,string createdBy="")
    {
        CreatedBy = createdBy;
        SurveyId = surveyId;
    }
    public int SurveyId { get; set; }
    public string CreatedBy { get; set; }

    public class Handler: IRequestHandler<GetSurveyQuery, List<Domain.Models.Survey>>
    {
        private readonly ISurveyAppDbContext _surveyAppDbContext;
        
        public Handler(ISurveyAppDbContext surveyAppDbContext) 
        {
        _surveyAppDbContext = surveyAppDbContext; 
        }

        public async Task<List<Domain.Models.Survey>> Handle(GetSurveyQuery request, CancellationToken cancellationToken)
        {
            var dbQuery= _surveyAppDbContext.Surveys.AsQueryable();

            if(request.SurveyId >0) 
            {
            dbQuery=dbQuery.Where(x=>x.Id == request.SurveyId);
            }
            if (!string.IsNullOrWhiteSpace(request.CreatedBy)) 
            {
            dbQuery=dbQuery.Where(x=>x.CreatedBy == request.CreatedBy);
            }
            var surveys = await dbQuery.Include(i => i.Options).ThenInclude(i => i.Votes).ToListAsync(cancellationToken); 
            return surveys;
        }
    }
}
