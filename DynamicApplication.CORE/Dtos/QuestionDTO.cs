using System.ComponentModel.DataAnnotations;

namespace DynamicApplication.CORE.Dtos
{
    public class QuestionDTO
    {
        public long QuestionId { get; set; }
        public string QuestionText { get; set; }

        public string Answer { get; set; } 
        public List<long>? MultipleChoiceAnswerId { get; set; }
    }

    public class EditQuestionDTO
    {
        public string QuestionText { get; set; }
        public long QuestionTypeId { get; set; }
        public List<string> MultipleChoiceAnswers { get; set; }
    }

    public class QuestionToReturnDTO
    {
        public long QuestionId { get; set; }
        public string QuestionText { get; set; }
    }
}
