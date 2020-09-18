namespace PRM.Domain.BaseCore.Dtos
{
    public class DomainResponseDto<TResult>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TResult Result { get; set; }
    }
}