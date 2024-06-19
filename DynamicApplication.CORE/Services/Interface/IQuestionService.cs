using DynamicApplication.CORE.Dtos;

namespace DynamicApplication.CORE.Services.Interface
{
    public interface IQuestionService
    {
        public Task<Result<object>> AddQuestion(AddQuestionDTO model);
        public Task<Result<object>> EditQuestion(long Id, EditQuestionDTO model);
        public Task<Result<List<QuestionToReturnDTO>>> GetQuestionsByTypeId(long questionTypeId);
    }
}
