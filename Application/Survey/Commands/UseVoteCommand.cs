using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Survey.Commands;

public  class UseVoteCommand : IRequest
{
	public UseVoteCommand (int surveyId, string usedBy,List<int> optionsIdList)
	{
		UsedBy = usedBy;
		OptionsIdList = optionsIdList;
		SurveyId=surveyId; 
	}
	public int SurveyId { get; set; }
	public string UsedBy { get; set;}
	public List<int> OptionsIdList { get; set; }

	public class Handler:IRequestHandler<UseVoteCommand>
	{
		private readonly ISurveyAppDbContext _surveyAppDbContext;
		public Handler(ISurveyAppDbContext surveyAppDbContext)
		{
			_surveyAppDbContext = surveyAppDbContext;
		}
		public async Task Handle(UseVoteCommand request, CancellationToken cancellationToken)
		{ 
		var survey= await _surveyAppDbContext.Surveys.Where(x=>x.Id==request.SurveyId).FirstOrDefaultAsync(cancellationToken);
		   
			if (survey is null) 
			{
				throw new Exception("Oy kullanmak istediğiniz anket bulunamadı. ");
			}

			var options = await _surveyAppDbContext.Options.Where(x => request.OptionsIdList.Contains(x.Id)).ToListAsync(cancellationToken);
			RunRule(request, options,survey);

			var vote=Vote.Create(request.UsedBy,options,survey);
			
			_surveyAppDbContext.Votes.Add(vote); 
			await _surveyAppDbContext.SaveChangesAsync(cancellationToken);
		}
		private void  RunRule(UseVoteCommand request,List<Option> options,Domain.Models.Survey survey)
		{
			if (request.OptionsIdList.Count != options.Count)
			{
				throw new Exception("Oy kullandığınız seçeneklerden bir kaçı bulunamadı.Lütfen tekrar deneyiniz.");
			}
			if(!survey.Settings.MultipleChoice&& request.OptionsIdList.Count>1) 
			{
				throw new Exception("Çoklu oy kullanımı kapalıdır.Lütfen tek oy kullanınız.");
			}
			if(survey.Settings.MinChoice> request.OptionsIdList.Count) 
			{
				throw new Exception($"Minimum {survey.Settings.MinChoice} adet seçim yapmalısınız. ");
			}
			if (survey.Settings.MaxChoice < request.OptionsIdList.Count)
			{
				throw new Exception($"Maksimum {survey.Settings.MinChoice} adet seçim yapmalısınız.");
			}
		}
	}
}
