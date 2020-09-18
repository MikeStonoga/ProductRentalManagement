
 using PRM.Domain.BaseCore.Dtos;

 namespace PRM.InterfaceAdapters.Gateways.Persistence.BaseCore
{
    public interface IPersistenceResponse<TResult>
    {
        TResult Response { get; set; }
    }
    
    public class PersistenceResponse<TResult> : ResultInfoDto, IPersistenceResponse<TResult>
    {
        public TResult Response { get; set; }
    }
}