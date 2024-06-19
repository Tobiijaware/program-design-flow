namespace DynamicApplication.DOMAIN.Models;

public class Question : BaseEntity
{
    public string QuestionText { get; set; }
    public QuestionType QuestionType { get; set; }
    public long AnswerId { get; set; }
    public List<Answer> Answers { get; set; }


    public long MultipleChoiceAnswerId { get; set; }
    public List<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }
}
