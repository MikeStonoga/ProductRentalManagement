﻿using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using PRM.Domain.BaseCore;
 using PRM.InterfaceAdapters.Controllers.BaseCore.Enums;
 using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Dtos;
 using PRM.UseCases.BaseCore;

 namespace PRM.InterfaceAdapters.Controllers.BaseCore.Extensions
{
    public static class ApiResponses
    {
        public static async Task<ApiResponse<GetAllResponse<TOutput>>> GetUseCaseInteractorResponse<TEntity, TOutput>(Func<Guid, Task<UseCaseResult<GetAllResponse<TEntity>>>> useCase, Guid input)
            where TEntity : FullAuditedEntity, new()
            where TOutput : class
        {
            var useCaseResponse = await useCase(input);
            if (!useCaseResponse.Success) return Failure<GetAllResponse<TOutput>>(useCaseResponse.Message);
            
            var outputs = new GetAllResponse<TOutput>
            {
                Items = new List<TOutput>()
            };
            
            foreach (var output in useCaseResponse.Result.Items.Select(entity => Activator.CreateInstance(typeof(TOutput), entity) as TOutput))
            {
                outputs.Items.Add(output);
            }

            outputs.TotalCount = useCaseResponse.Result.TotalCount;
            return Success(outputs, useCaseResponse.Message);
        }
        
        public static async Task<ApiResponse<GetAllResponse<TOutput>>> GetUseCaseInteractorResponse<TEntity, TOutput>(Func<Task<UseCaseResult<GetAllResponse<TEntity>>>> useCase)
            where TOutput : class
        {
            var useCaseResponse = await useCase();
            if (!useCaseResponse.Success) return Failure<GetAllResponse<TOutput>>(useCaseResponse.Message);
            
            var outputs = new GetAllResponse<TOutput>
            {
                Items = new List<TOutput>()
            };
            
            foreach (var output in useCaseResponse.Result.Items.Select(entity => Activator.CreateInstance(typeof(TOutput), entity) as TOutput))
            {
                outputs.Items.Add(output);
            }

            outputs.TotalCount = useCaseResponse.Result.TotalCount;
            return Success(outputs, useCaseResponse.Message);
        }
        
        public static async Task<ApiResponse<TOutput>> GetUseCaseInteractorResponse<TUseCaseRequirement, TUseCaseResult, TInput, TOutput>(Func<TUseCaseRequirement, Task<UseCaseResult<TUseCaseResult>>> useCase, TInput input)
            where TInput : TUseCaseRequirement 
            where TOutput : class, TUseCaseResult, new()
        {
            var useCaseResponse = await useCase(input);
            if (!useCaseResponse.Success) return Failure<TOutput>(useCaseResponse.Message);
            
            var output = Activator.CreateInstance(typeof(TOutput), useCaseResponse.Result) as TOutput;
            return Success(output, useCaseResponse.Message);
        }
        
        public static ApiResponse<TResult> Success<TResult>(TResult result, string message = "")
        {
            return ExecutionStatus.ExecutedSuccessfully.GetSuccessResult(result, message);
        }
        
        public static ApiResponse<TResult> Failure<TResult>(TResult result, string message = "")
        {
            return ExecutionStatus.Failure.GetFailureResult(result, message);
        }
        
        public static ApiResponse<TResult> Failure<TResult>(string message = "") where TResult : new()
        {
            var result = new TResult();
            return ExecutionStatus.Failure.GetFailureResult(result, message);
        }

        public static ApiResponse<TResult> GetSuccessResult<TEnum, TResult>(this TEnum resultEnum, TResult result, string message = "") 
            where TEnum : Enum 
        {
            return resultEnum.GetResponse(true, result, message);
        }
        
        public static ApiResponse<TResult> GetFailureResult<TEnum, TResult>(this TEnum resultEnum, TResult result, string message = "") 
            where TEnum : Enum 
        {
            return resultEnum.GetResponse(false, result, message);
        }
        
        
        public static ApiResponse<TResult> GetResponse<TEnum, TResult>(this TEnum resultEnum, bool isSuccessResult, TResult response, string message = "") 
            where TEnum : Enum
        {
            if (string.IsNullOrEmpty(message))
            {
                message = resultEnum.ToString();
            }
            
            return new ApiResponse<TResult>
            {
                Success = isSuccessResult,
                Message = message,
                ErrorCodeName = resultEnum.ToString(),
                Response = response
            };
        }
    }
}