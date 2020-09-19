using System;

namespace PRM.Domain.BaseCore.Dtos
{
    public class DomainResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    
    public class DomainResponseDto<TResult> where TResult : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TResult Result { get; set; }
    }
}