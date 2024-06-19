using DynamicApplication.CORE.Dtos;
using DynamicApplication.CORE.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DynamicApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost("createapplication")]
        public async Task<IActionResult> Create([FromBody] ApplicationDTO applicationDTO)
        {
            if(!ModelState.IsValid) 
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                     .Select(e => new Error("ModelValidationError", e.ErrorMessage))
                                     .ToList();

                return BadRequest(ResponseDTO<object>.Failure(errors));
            };

            var result = await _applicationService.CreateApplication(applicationDTO);

            if (result.IsFailure)
                return BadRequest(ResponseDTO<object>.Failure(result.Errors));

            return Ok(ResponseDTO<object>.Success());
        }

        
    }
}
