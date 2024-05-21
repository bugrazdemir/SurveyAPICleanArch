namespace WebApi.Models.Request;

public class OptionCreateRequest
{
    public string Type { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
}