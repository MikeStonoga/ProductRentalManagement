using PRM.Domain.BaseCore.Dtos;

 namespace PRM.InterfaceAdapters.Controllers.BaseCore
{
    public interface IApiResponse<TResponse> : IResultInfoDto
    {
        public TResponse Response { get; set; }
    }
    
    public class ApiResponse<TResponse> : ResultInfoDto, IApiResponse<TResponse>
    {
        public TResponse Response { get; set; }
    }
}