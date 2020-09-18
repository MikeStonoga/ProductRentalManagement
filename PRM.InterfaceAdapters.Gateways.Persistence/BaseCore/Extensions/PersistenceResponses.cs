using System;

namespace PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Extensions
{
    public static class PersistencesResponses
    {
        public static PersistenceResponse<TResponse> GetSuccessResponse<TEnum, TResponse>(this TEnum resultEnum, TResponse result, string message = "") 
            where TEnum : Enum 
        {
            return resultEnum.GetResponse(true, result, message);
        }
        
        public static PersistenceResponse<TResponse> GetFailureResponse<TEnum, TResponse>(this TEnum resultEnum, string message = "") 
            where TEnum : Enum 
            where TResponse : class, new()
        {
            var result = new TResponse();
            return resultEnum.GetResponse(false, result, message);
        }
        
        public static PersistenceResponse<TResponse> GetResponse<TEnum, TResponse>(this TEnum resultEnum, bool isSuccessResponse, TResponse result, string message = "") 
            where TEnum : Enum
        {
            if (string.IsNullOrEmpty(message))
            {
                message = resultEnum.ToString();
            }
            
            return new PersistenceResponse<TResponse>
            {
                Success = isSuccessResponse,
                Message = message,
                ErrorCodeName = resultEnum.ToString(),
                Response = result
            };
        }
    }
}