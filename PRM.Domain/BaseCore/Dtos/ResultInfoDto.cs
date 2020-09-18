namespace PRM.Domain.BaseCore.Dtos
{
    public interface IResultInfoDto
    {
        public bool Success { get; set; }
        public string ErrorCodeName { get; set; }
        public string Message { get; set; }
    }
    
    public abstract class ResultInfoDto
    {
        public bool Success { get; set; }
        public string ErrorCodeName { get; set; }
        public string Message { get; set; }
    }
}