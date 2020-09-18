﻿using System;
 using System.Threading.Tasks;
 using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
 using PRM.UseCases.BaseCore.Enums;

 namespace PRM.UseCases.BaseCore.Extensions
{
    public static class UseCasesResponses
    {
        public static async Task<UseCaseResult<TUseCaseResult>> GetUseCaseExecutionResponse<TIUseCase, TUseCaseResult>(TIUseCase useCase)
            where TIUseCase : IBaseUseCase<TUseCaseResult> where TUseCaseResult : class, new()
        {
            var useCaseResponse = await useCase.Execute();
            if (!useCaseResponse.Success) return Failure<TUseCaseResult>(useCaseResponse.Message);
            var useCaseResult = useCaseResponse.Result;
            if (useCaseResult.GetType() == typeof(TUseCaseResult)) return Success(useCaseResponse.Result, useCaseResponse.Message);
            
            var output = Activator.CreateInstance(typeof(TUseCaseResult), useCaseResponse.Result) as TUseCaseResult;
            return Success(output, useCaseResponse.Message);
        }
        
        public static async Task<UseCaseResult<TUseCaseResult>> GetUseCaseExecutionResponse<TIUseCase, TUseCaseRequirement, TUseCaseResult>(TIUseCase useCase, TUseCaseRequirement input)
            where TIUseCase : IBaseUseCase<TUseCaseRequirement, TUseCaseResult> where TUseCaseResult : class, new()
        {
            var useCaseResponse = await useCase.Execute(input);
            if (!useCaseResponse.Success) return Failure<TUseCaseResult>(useCaseResponse.Message);
            var useCaseResult = useCaseResponse.Result;
            if (useCaseResult.GetType() == typeof(TUseCaseResult)) return Success(useCaseResponse.Result, useCaseResponse.Message);
            
            var output = Activator.CreateInstance(typeof(TUseCaseResult), useCaseResponse.Result) as TUseCaseResult;
            return Success(output, useCaseResponse.Message);
        }
        
        public static UseCaseResult<TResult> GetUseCaseResult<TResult>(PersistenceResponse<TResult> persistenceResponse)
        {
            var wasSuccessfullyExecuted = persistenceResponse.Success;
            
            return wasSuccessfullyExecuted
                ? Success(persistenceResponse.Response)
                : PersistenceErrorResponse(persistenceResponse.Response, persistenceResponse.Message);
        }

        
        public static UseCaseResult<TResult> Success<TResult>(TResult result, string message = "")
        {
            return UseCaseResults.UseCaseSuccessfullyExecuted.GetSuccessResult(result, message);
        }
        
        public static UseCaseResult<TResult> Success<TResult>(string message = "") where TResult : new()
        {
            var result = new TResult();
            return UseCaseResults.UseCaseSuccessfullyExecuted.GetSuccessResult(result, message);
        }
        
        public static UseCaseResult<TResult> Failure<TResult>(TResult result, string message = "") where TResult : new()
        {
            return UseCaseResults.UseCaseFailureExecution.GetFailureResult(result, message);
        }
        
        public static UseCaseResult<TResult> Failure<TResult>(string message = "") where TResult : new()
        {
            var result = new TResult();
            return UseCaseResults.UseCaseFailureExecution.GetFailureResult(result, message);
        }
        
        public static UseCaseResult<TResult> ValidationErrorResponse<TResult>(TResult result, string message = "")
        {
            return UseCaseResults.ValidationError.GetFailureResult(result, message);
        }
        
        public static UseCaseResult<TResult> ValidationErrorResponse<TResult>(string message = "") where TResult : new()
        {
            var result = new TResult();
            return UseCaseResults.PersistenceError.GetFailureResult(result, message);
        }
        
        public static UseCaseResult<TResult> PersistenceErrorResponse<TResult>(TResult result, string message = "")
        {
            return UseCaseResults.PersistenceError.GetFailureResult(result, message);
        }
        
        public static UseCaseResult<TResult> PersistenceErrorResponse<TResult>(string message = "") where TResult : new()
        {
            var result = new TResult();
            return UseCaseResults.PersistenceError.GetFailureResult(result, message);
        }

        public static UseCaseResult<TResult> GetSuccessResult<TEnum, TResult>(this TEnum resultEnum, TResult result, string message = "") 
            where TEnum : Enum 
        {
            return resultEnum.GetResult(true, result, message);
        }
        
        public static UseCaseResult<TResult> GetFailureResult<TEnum, TResult>(this TEnum resultEnum, TResult result, string message = "") 
            where TEnum : Enum 
        {
            return resultEnum.GetResult(false, result, message);
        }
        
        public static UseCaseResult<TResult> GetResult<TEnum, TResult>(this TEnum resultEnum, bool isSuccessResult, TResult result, string message = "") 
            where TEnum : Enum
        {
            if (string.IsNullOrEmpty(message))
            {
                message = resultEnum.ToString();
            }
            
            return new UseCaseResult<TResult>
            {
                Success = isSuccessResult,
                Message = message,
                ErrorCodeName = resultEnum.ToString(),
                Result = result
            };
        }
    }
}