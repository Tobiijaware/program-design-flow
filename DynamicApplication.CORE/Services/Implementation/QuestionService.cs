using DynamicApplication.CORE.Dtos;
using DynamicApplication.CORE.Services.Interface;
using DynamicApplication.DOMAIN.Models;
using DynamicApplication.INFRA.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DynamicApplication.CORE.Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IApplicationRepository _applicationRepository;
        public QuestionService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Result<object>> AddQuestion(AddQuestionDTO model)
        {
            try
            {
                var type = await _applicationRepository.FindById<QuestionType>(model.QuestionTypeId);
                if(type == null)
                {
                    return Result.Failure<object>(new Error[] { new("Question.Error", $"Question Not Found") });
                }

                var question = new Question
                {
                    QuestionText = model.QuestionText,
                    QuestionType = type
                };

                if(type.QuestionTypeName == "multiplechoice" && model.MultipleChoiceAnswers != null && model.MultipleChoiceAnswers.Any())
                {
                    foreach(var answer in model.MultipleChoiceAnswers)
                    {
                        var mult = new MultipleChoiceAnswer
                        {
                            Question = question,
                            QuestionId = question.Id,
                            AnswerText = answer,
                        };

                        await _applicationRepository.Add<MultipleChoiceAnswer>(mult);
                    }
                }

                await _applicationRepository.Add<Question>(question);
                await _applicationRepository.SaveChangesAsync();
                return Result.Success(new { Id = question.Id});

            }
            catch(Exception ex)
            {
                return Result.Failure<object>(new Error[] { new("Question.Error", $"{ex.Message}") });
            }
        }

        public async Task<Result<object>> EditQuestion(long Id, EditQuestionDTO model)
        {
            try
            {
                var question = await _applicationRepository.FindById<Question>(Id);
                if (question == null)
                {
                    return Result.Failure<object>(new Error[] { new("Question.Error", "Question Not Found") });
                }

                var type = await _applicationRepository.FindById<QuestionType>(model.QuestionTypeId);
                if (type == null)
                {
                    return Result.Failure<object>(new Error[] { new("Question.Error", "Question Type Not Found") });
                }

                question.QuestionText = model.QuestionText;
                question.QuestionType = type;

                if (type.QuestionTypeName == "multiplechoice" && model.MultipleChoiceAnswers != null)
                {
                    
                    var existingAnswers = question.MultipleChoiceAnswers.ToList();

                    foreach (var answer in existingAnswers)
                    {
                         _applicationRepository.Remove<MultipleChoiceAnswer>(answer);
                    }
                    question.MultipleChoiceAnswers.Clear();

                    // Add new multiple choice answers
                    foreach (var answerText in model.MultipleChoiceAnswers)
                    {
                        var mult = new MultipleChoiceAnswer
                        {
                            Question = question,
                            AnswerText = answerText
                        };
                        question.MultipleChoiceAnswers.Add(mult);
                        await _applicationRepository.Add<MultipleChoiceAnswer>(mult);
                    }

                }

                _applicationRepository.Update<Question>(question);
                await _applicationRepository.SaveChangesAsync();

                return Result.Success(new { Id = question.Id });
            }
            catch (Exception ex)
            {
                return Result.Failure<object>(new Error[] { new("Question.Error", $"{ex.Message}") });
            }
        }

        public async Task<Result<List<QuestionToReturnDTO>>> GetQuestionsByTypeId(long questionTypeId)
        {
            try
            {
                var questions = _applicationRepository.GetAll<Question>().
                    Include(q => q.Answers)
                    .Include(m => m.MultipleChoiceAnswers)
                    .Where(i => i.QuestionType.Id == questionTypeId);

                var questionDTOs = questions.Select(q => new QuestionToReturnDTO
                {
                    QuestionId = q.Id,
                    QuestionText = q.QuestionText,
                }).ToList();

                return Result.Success(questionDTOs);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<QuestionToReturnDTO>>(new Error[] { new("Question.Error", $"{ex.Message}") });
            }
        }

    }
}
