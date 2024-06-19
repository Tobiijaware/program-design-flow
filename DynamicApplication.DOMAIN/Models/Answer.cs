namespace DynamicApplication.DOMAIN.Models
{
    public class Answer : BaseEntity
    {
        public string AnswerText { get; set; }
        //public QuestionType QuestionType { get; set; }
        public long QuestionId { get; set; } 
        public Question Question { get; set; }
        public string? CandidateEmail { get; set; }
    }
}
