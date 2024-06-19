using DynamicApplication.CORE.Dtos;
using DynamicApplication.CORE.Services.Implementation;
using DynamicApplication.CORE.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DynamicApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("createquestion")]
        public async Task<IActionResult> Create([FromBody] AddQuestionDTO questionDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                     .Select(e => new Error("ModelValidationError", e.ErrorMessage))
                                     .ToList();

                return BadRequest(ResponseDTO<object>.Failure(errors));
            };

            var result = await _questionService.AddQuestion(questionDTO);

            if (result.IsFailure)
                return BadRequest(ResponseDTO<object>.Failure(result.Errors));

            return Ok(ResponseDTO<object>.Success());
        }

        [HttpPut("editquestion/{questionId}")]
        public async Task<IActionResult> Edit([FromRoute] long questionId, [FromBody] EditQuestionDTO questionDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                     .Select(e => new Error("ModelValidationError", e.ErrorMessage))
                                     .ToList();

                return BadRequest(ResponseDTO<object>.Failure(errors));
            };

            var result = await _questionService.EditQuestion(questionId, questionDTO);

            if (result.IsFailure)
                return BadRequest(ResponseDTO<object>.Failure(result.Errors));

            return Ok(ResponseDTO<object>.Success());
        }

        [HttpGet("questions/bytype/{questionTypeId}")]
        public async Task<IActionResult> GetQuestionsByTypeId(long questionTypeId)
        {
            var result = await _questionService.GetQuestionsByTypeId(questionTypeId);

            if (result.IsSuccess)
            {
                return Ok(ResponseDTO<List<QuestionToReturnDTO>>.Success(result.Data));
            }
            else
            {
                return BadRequest(ResponseDTO<List<QuestionToReturnDTO>>.Failure(result.Errors));
            }
        }
    }
}
