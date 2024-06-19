namespace DynamicApplication.DOMAIN.Models
{
    public class MultipleChoiceAnswer : BaseEntity
    {
        public string AnswerText { get; set; }
        public long QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
