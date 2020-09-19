﻿using System;
using System.Collections.Generic;
using System.Linq;
 using System.Threading.Tasks;
 using Microsoft.AspNetCore.Mvc;
 using PRM.Domain.BaseCore;
using PRM.Domain.BaseCore.Enums;
 using PRM.InterfaceAdapters.Controllers.BaseCore.Extensions;
using PRM.UseCases.BaseCore;

namespace PRM.InterfaceAdapters.Controllers.BaseCore
{
    public interface IBaseReadOnlyController<TEntity, TEntityOutput>
        where TEntity : FullAuditedEntity
    {
    }
    
    public class GetAllResponse<TEntityOutput>
    {
        public List<TEntityOutput> Items { get; set; }
        public int TotalCount { get; set; }
    }
    
    public interface IBaseManipulationController<TEntity, in TEntityInput, TEntityOutput> : IBaseReadOnlyController<TEntity, TEntityOutput> 
        where TEntity : FullAuditedEntity
        where TEntityOutput : class
    {
    }

    public abstract class BaseReadOnlyController<TEntity, TEntityOutput, TIEntityUseCaseReadOnlyInteractor> : ControllerBase, IBaseReadOnlyController<TEntity, TEntityOutput> 
        where TEntity : FullAuditedEntity
        where TEntityOutput : class
        where TIEntityUseCaseReadOnlyInteractor : IBaseUseCaseReadOnlyInteractor<TEntity>
    {

        protected readonly TIEntityUseCaseReadOnlyInteractor UseCaseReadOnlyInteractor;

        protected BaseReadOnlyController(TIEntityUseCaseReadOnlyInteractor useCaseReadOnlyInteractor)
        {
            UseCaseReadOnlyInteractor = useCaseReadOnlyInteractor;
        }

        public virtual async Task<ApiResponse<TEntityOutput>> GetById(Guid id)
        {
            var useCaseResult = await UseCaseReadOnlyInteractor.GetById(id);
            var wasSuccessfullyExecuted = useCaseResult.Success;
            if (!wasSuccessfullyExecuted) return ApiResponses.Failure(Activator.CreateInstance<TEntityOutput>(), useCaseResult.Message);

            var entityOutput = Activator.CreateInstance(typeof(TEntityOutput), useCaseResult.Result) as TEntityOutput;
            return ApiResponses.Success(entityOutput);
        }


        public virtual async Task<ApiResponse<List<TEntityOutput>>> GetByIds(List<Guid> ids)
        {
            var useCaseResult = await UseCaseReadOnlyInteractor.GetByIds(ids);
            var wasSuccessfullyExecuted = useCaseResult.Success;

            var entityOutputs = useCaseResult.Result.Select(result => Activator.CreateInstance(typeof(TEntityOutput), result) as TEntityOutput).ToList();

            return wasSuccessfullyExecuted
                ? ApiResponses.Success(entityOutputs)
                : ApiResponses.Failure(entityOutputs, useCaseResult.Message);
        }

        public virtual async Task<ApiResponse<GetAllResponse<TEntityOutput>>> GetAll()
        {
            return await GetAll(null);
        }
        
        protected async Task<ApiResponse<GetAllResponse<TEntityOutput>>> GetAll(Func<TEntity, bool> whereExpression = null)
        {
            var useCaseResult = await UseCaseReadOnlyInteractor.GetAll(whereExpression);
            var wasSuccessfullyExecuted = useCaseResult.Success;
            if (!wasSuccessfullyExecuted)
            {
                var failureResult = new GetAllResponse<TEntityOutput>
                {
                    Items = new List<TEntityOutput>(),
                    TotalCount = 0
                };
                return ApiResponses.Failure(failureResult, useCaseResult.Message);
            }
            
            var entityOutputs = useCaseResult.Result.Items.Select(result => Activator.CreateInstance(typeof(TEntityOutput), result) as TEntityOutput).ToList();
            
            var getAllOutput = new GetAllResponse<TEntityOutput>
            {
                Items = entityOutputs,
                TotalCount = useCaseResult.Result.TotalCount
            };
            
            return ApiResponses.Success(getAllOutput);
        }
    }

    public abstract class BaseManipulationController<TEntity, TEntityInput, TEntityOutput, TIEntityUseCaseManipulationInteractor, TIEntityReadOnlyController> : BaseReadOnlyController<TEntity, TEntityOutput, TIEntityUseCaseManipulationInteractor>,  IBaseManipulationController<TEntity, TEntityInput, TEntityOutput>
        where TEntity : FullAuditedEntity
        where TEntityOutput : class
        where TEntityInput : TEntity, IAmManipulationInput<TEntity>
        where TIEntityUseCaseManipulationInteractor : IBaseUseCaseManipulationInteractor<TEntity>
        where TIEntityReadOnlyController : IBaseReadOnlyController<TEntity, TEntityOutput>
    {
        protected readonly TIEntityUseCaseManipulationInteractor UseCaseInteractor;
        protected readonly TIEntityReadOnlyController ReadOnlyController;

        protected BaseManipulationController(TIEntityUseCaseManipulationInteractor useCaseInteractor, TIEntityReadOnlyController readOnlyController) : base(useCaseInteractor)
        {
            UseCaseInteractor = useCaseInteractor;
            ReadOnlyController = readOnlyController;
        }
        
        protected internal async Task<ApiResponse<TEntityOutput>> Create(TEntityInput input, Guid creatorId)
        {
            var entity = input.MapToEntity();
            
            entity.Id = input.Id;
            entity.Code = input.Code;
            entity.Name = input.Name;
            entity.CreatorId = creatorId;
            var useCaseResult = await UseCaseInteractor.Create(entity);
            
            var wasSuccessfullyExecuted = useCaseResult.Success;
            if (!wasSuccessfullyExecuted) return ApiResponses.Failure(Activator.CreateInstance<TEntityOutput>(), useCaseResult.Message);

            var entityOutput = Activator.CreateInstance(typeof(TEntityOutput), useCaseResult.Result) as TEntityOutput;
            return ApiResponses.Success(entityOutput);
        }

        protected internal async Task<ApiResponse<TEntityOutput>> Update(TEntityInput entityToUpdate, Guid modifierId)
        {
            var entity = entityToUpdate.MapToEntity();
            
            entity.Id = entityToUpdate.Id;
            entity.Code = entityToUpdate.Code;
            entity.Name = entityToUpdate.Name;
            entity.CreatorId = modifierId;
            var useCaseResult = await UseCaseInteractor.Update(entity);
            
            var wasSuccessfullyExecuted = useCaseResult.Success;
            if (!wasSuccessfullyExecuted) return ApiResponses.Failure(Activator.CreateInstance<TEntityOutput>(), useCaseResult.Message);

            var entityOutput = Activator.CreateInstance(typeof(TEntityOutput), useCaseResult.Result) as TEntityOutput;
            return ApiResponses.Success(entityOutput);
        }

        public virtual async Task<ApiResponse<DeletionResponses>> Delete(Guid id)
        {
            var useCaseResult = await UseCaseInteractor.Delete(id);
            return !useCaseResult.Success ? DeletionResponses.DeletionFailure.GetFailureResult(useCaseResult.Result) : DeletionResponses.DeleteSuccessfully.GetSuccessResult(useCaseResult.Result);
        }
    }
}