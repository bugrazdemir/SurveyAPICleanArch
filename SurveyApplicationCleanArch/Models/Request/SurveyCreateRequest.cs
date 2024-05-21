using Domain.Models;

namespace WebApi.Models.Request;

public class SurveyCreateRequest
{
    public string Question { get; set; }
    public string CreatedBy { get; set; }
    public List<OptionCreateRequest> Options { get; set; }
    public Settings Settings { get; set; }
}