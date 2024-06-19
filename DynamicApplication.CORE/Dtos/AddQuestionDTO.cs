using System.ComponentModel.DataAnnotations;

namespace DynamicApplication.CORE.Dtos
{
    public class AddQuestionDTO
    {
        [Required(ErrorMessage = "Question Type Is Required")]
        public long QuestionTypeId { get; set; }

        [Required(ErrorMessage = "Question Text Is Required")]
        public string QuestionText { get; set; }

        public List<string>? MultipleChoiceAnswers { get; set; }
    }
}
