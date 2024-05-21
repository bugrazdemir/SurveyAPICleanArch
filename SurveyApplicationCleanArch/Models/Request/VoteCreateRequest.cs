using Application.Survey.Commands;

namespace WebApi.Models.Request;

public class VoteCreateRequest
{
    public string UsedBy { get; set; }
    public List<int> OptionIdList { get; set; }

    public UseVoteCommand ToCommand(int surveyId)
    {
        return new UseVoteCommand(surveyId, UsedBy, OptionIdList);
    }
}