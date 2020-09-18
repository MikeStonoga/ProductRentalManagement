using PRM.Domain.BaseCore.Dtos;

namespace PRM.UseCases.BaseCore
{
    public interface IUseCaseResult<TResult> : IResultInfoDto
    {
        public TResult Result { get; set; }
    }
    
    public class UseCaseResult<TResult> : ResultInfoDto, IUseCaseResult<TResult>
    {
        public TResult Result { get; set; }
    }
}