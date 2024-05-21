using Microsoft.VisualBasic;
using System.Data;

namespace Domain.Models;
public class Survey:BaseModel{

    public Survey()
    {
        //only db operations
    }

    private Survey(string question, string createdBy, Settings settings, List<Option> options)
    {
        if (string.IsNullOrWhiteSpace(createdBy))
        {
            createdBy = "admin";
        }
        Question = question;
        CreatedBy = createdBy;
        Settings = settings;
        Options = options;
        DueDate = DateTime.Now.AddDays(1);
        CreatedDate = DateTime.Now;
    }
    public string Question { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public Settings Settings { get; private set; }

    public virtual List<Option> Options { get; set; }
    public virtual List<Vote> Votes { get; set; }

    public static Survey Create(string question, string createdBy, Settings settings, List<Option> options)
    {
        return new Survey(question, createdBy, settings, options);
    }
    public Survey Update()
    {
        return this;
    }
}




