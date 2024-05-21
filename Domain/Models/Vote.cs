
namespace Domain.Models
{
    public  class Vote : BaseModel
    {
        public Vote() { 
        //only ef db operations
        }
        private Vote(string user,List<Option> options,Survey survey) {
            User = user;
            Options = options;
            Survey = survey;
            SurveyId=survey.Id; 
        }
        public int SurveyId { get; set; }
        public string User { get; set; }
        public virtual List<Option> Options { get; set; }
        public virtual Survey Survey { get; set; }
        public static Vote Create(string user,List<Option>options,Survey survey)
        {
            return new Vote(user,options,survey);
        }
    }
}
