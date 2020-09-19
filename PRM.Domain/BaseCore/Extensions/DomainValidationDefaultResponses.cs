using System;
using PRM.Domain.BaseCore.Dtos;

namespace PRM.Domain.BaseCore.Extensions
{
    public static class DomainValidations
    {
        public static DomainResponseDto<TResult> GetSuccessResponse<TResult>(this TResult result, string message) where TResult : class
        {
            return new DomainResponseDto<TResult>
            {
                Success = true,
                Message = message,
                Result = result
            };
        }
        
        
        public static DomainResponseDto<TResult> Failure<TResult>(string message) where TResult : class
        {
            var result = Activator.CreateInstance<TResult>();
            return new DomainResponseDto<TResult>
            {
                Success = false,
                Message = message,
                Result = result
            };
        }
    }
}