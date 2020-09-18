﻿﻿using PRM.Domain.BaseCore;

namespace PRM.InterfaceAdapters.Controllers.BaseCore
{
    public interface IAmManipulationInput<TEntity> where TEntity : FullAuditedEntity
    {
        TEntity MapToEntity();
    };
}