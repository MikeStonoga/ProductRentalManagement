﻿using System;
using System.Collections.Generic;
 using System.Linq.Expressions;
 using System.Threading.Tasks;
using PRM.Domain.BaseCore;
 using PRM.Domain.BaseCore.Enums;
 using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
 using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Dtos;
 using PRM.UseCases.BaseCore.Extensions;

namespace PRM.UseCases.BaseCore
{
    public interface IBaseUseCaseReadOnlyInteractor<TEntity>
        where TEntity : Entity
    {
        Task<UseCaseResult<TEntity>> GetById(Guid id);
        Task<UseCaseResult<List<TEntity>>> GetByIds(List<Guid> ids);
        Task<UseCaseResult<GetAllResponse<TEntity>>> GetAll(Func<TEntity, bool> whereExpression = null);
    }
    public interface IBaseUseCaseManipulationInteractor<TEntity> : IBaseUseCaseReadOnlyInteractor<TEntity>
        where TEntity : Entity
    {
        Task<UseCaseResult<TEntity>> Create(TEntity userToCreate);
        Task<UseCaseResult<TEntity>> Update(TEntity entity);
        Task<UseCaseResult<DeletionResponses>> Delete(Guid id);
    }

    public class BaseUseCaseReadOnlyInteractor<TEntity> : IBaseUseCaseReadOnlyInteractor<TEntity>
        where TEntity : FullAuditedEntity
    {
        protected readonly IReadOnlyPersistenceGateway<TEntity> ReadOnlyPersistenceGateway;

        public BaseUseCaseReadOnlyInteractor(IReadOnlyPersistenceGateway<TEntity> readOnlyPersistenceGateway)
        {
            ReadOnlyPersistenceGateway = readOnlyPersistenceGateway;
        }
        

        public virtual async Task<UseCaseResult<TEntity>> GetById(Guid id)
        {
            var persistenceResponse = await ReadOnlyPersistenceGateway.GetById(id);

            return UseCasesResponses.GetUseCaseResult(persistenceResponse);
        }

        public virtual async Task<UseCaseResult<List<TEntity>>> GetByIds(List<Guid> ids)
        {
            var persistenceResponse = await ReadOnlyPersistenceGateway.GetByIds(ids);
            return UseCasesResponses.GetUseCaseResult(persistenceResponse);
        }

        public async Task<UseCaseResult<GetAllResponse<TEntity>>> GetAll(Func<TEntity, bool> whereExpression = null)
        {
            var persistenceResponse = await ReadOnlyPersistenceGateway.GetAll(whereExpression);
            return UseCasesResponses.GetUseCaseResult(persistenceResponse);
        }
    }
    
    public class BaseUseCaseManipulationInteractor<TEntity, TIEntityUseCasesReadOnlyInteractor> : BaseUseCaseReadOnlyInteractor<TEntity>, IBaseUseCaseManipulationInteractor<TEntity>
        where TEntity : FullAuditedEntity
        where TIEntityUseCasesReadOnlyInteractor : IBaseUseCaseReadOnlyInteractor<TEntity>
    {
        protected readonly TIEntityUseCasesReadOnlyInteractor UseCasesReadOnlyInteractor;
        private readonly IManipulationPersistenceGateway<TEntity> _persistenceGateway;
        
        public BaseUseCaseManipulationInteractor(IManipulationPersistenceGateway<TEntity> persistenceGateway, TIEntityUseCasesReadOnlyInteractor useCasesReadOnlyInteractor) : base(persistenceGateway)
        {
            _persistenceGateway = persistenceGateway;
            UseCasesReadOnlyInteractor = useCasesReadOnlyInteractor;
        }
        
        
        public virtual async Task<UseCaseResult<TEntity>> Create(TEntity userToCreate)
        {
            var persistenceResponse = await _persistenceGateway.Create(userToCreate);
            return UseCasesResponses.GetUseCaseResult(persistenceResponse);
        }

        public virtual async Task<UseCaseResult<TEntity>> Update(TEntity entity)
        {
            var persistenceResponse = await _persistenceGateway.Update(entity);
            return UseCasesResponses.GetUseCaseResult(persistenceResponse);
        }

        public virtual async Task<UseCaseResult<DeletionResponses>> Delete(Guid id)
        {
            var persistenceResponse = await _persistenceGateway.Delete(id);
            return UseCasesResponses.GetUseCaseResult(persistenceResponse);
        }
    }
}