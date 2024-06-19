using DynamicApplication.CORE.Dtos;
using DynamicApplication.CORE.Services.Interface;
using DynamicApplication.DOMAIN.Models;
using DynamicApplication.INFRA.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DynamicApplication.CORE.Services.Implementation
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Result<object>> CreateApplication(ApplicationDTO applicationDTO)
        {
            try
            {
                var form = new Form
                {
                    Title = applicationDTO.Title,
                    Description = applicationDTO.Description,
                    FirstName = applicationDTO.FirstName,
                    LastName = applicationDTO.LastName,
                    EmailAddress = applicationDTO.EmailAddress,
                    PhoneNumber = applicationDTO.PhoneNumber,
                    Nationality = applicationDTO.Nationality,
                    CurrentResidence = applicationDTO.CurrentResidence,
                    IdNumber = applicationDTO.IdNumber,
                    DateOfBirth = applicationDTO.DateOfBirth,
                    Gender = applicationDTO.Gender,
                    Question = new List<Question>()
                };

                foreach (var questionDto in applicationDTO.Questions)
                {
                    //get the question
                    var question =  _applicationRepository.GetAll<Question>()
                        .Include(q => q.QuestionType)
                        .FirstOrDefault(x => x.Id == questionDto.QuestionId);

                    if (question == null)
                    {
                        return Result.Failure<object>(new Error[] { new("Form.Error", $"Question Not Found") });
                    }

                    //if question type is multiple choice
                    if (question.QuestionType.QuestionTypeName == "multiplechoice")
                    {
                        if (questionDto.MultipleChoiceAnswerId != null && questionDto.MultipleChoiceAnswerId.Count > 0)
                        {
                            foreach (var multipleChoiceAnswerId in questionDto.MultipleChoiceAnswerId)
                            {
                                var verify = await _applicationRepository.FindById<MultipleChoiceAnswer>(multipleChoiceAnswerId);

                                if(verify == null)
                                {
                                    return Result.Failure<object>(new Error[] { new("Form.Error", $"Answer Can Not Found") });
                                }

                                var ans = new Answer
                                {
                                    AnswerText = verify.AnswerText,
                                    Question = question,
                                    QuestionId = questionDto.QuestionId,
                                };

                                await _applicationRepository.Add<Answer>(ans);
                            }
                        }

                    }
                    else
                    {
                        var answer = new Answer
                        {
                            AnswerText = questionDto.Answer,
                            QuestionId = questionDto.QuestionId,
                            Question = question
                        };

                        await _applicationRepository.Add<Answer>(answer);
                    }
                }

                await _applicationRepository.Add<Form>(form);
                await _applicationRepository.SaveChangesAsync();

                return Result.Success(new { Id = form.Id });

            }catch (Exception ex)
            {
                return Result.Failure<object>(new Error[] { new("Form.Error", $"{ex.Message}") });
            }
        }

    }
}
