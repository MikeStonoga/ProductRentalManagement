using System.Collections.Generic;
using PRM.Domain.BaseCore;

namespace PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Dtos
{
    public class GetAllResponse<TEntity> where TEntity : Entity
    {
        public List<TEntity> Items { get; set; }
        public int TotalCount { get; set; }
    }
}