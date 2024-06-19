using DynamicApplication.CORE.Dtos;

namespace DynamicApplication.CORE.Services.Interface
{
    public interface IApplicationService
    {
        Task<Result<object>> CreateApplication(ApplicationDTO applicationDTO);
    }
}
