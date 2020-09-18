using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PRM.Domain.BaseCore;
using PRM.Domain.BaseCore.Enums;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Dtos;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Enums;
using PRM.InterfaceAdapters.Gateways.Persistence.BaseCore.Extensions;

namespace PRM.Infrastructure.Persistence.MySQL
{
    public interface IReadOnlyRepository<TEntity> : IReadOnlyPersistenceGateway<TEntity> where TEntity : FullAuditedEntity
    {
    }
    
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : FullAuditedEntity, new()
    {
        private readonly DbContext _database;

        public ReadOnlyRepository(ICurrentDbContext database)
        {
            _database = database.Context;

        }

        public async Task<PersistenceResponse<TEntity>> GetById(Guid id)
        {
            try
            {
                var entity = await _database.Set<TEntity>().FindAsync(id);

                return entity.IsDeleted 
                    ? PersistenceResponseStatus.Success.GetFailureResponse<PersistenceResponseStatus, TEntity>("WasDeleted") 
                    : PersistenceResponseStatus.Success.GetSuccessResponse(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return PersistenceResponseStatus.PersistenceFailure.GetFailureResponse<PersistenceResponseStatus, TEntity>(e.Message);
            }
        }

        public async Task<PersistenceResponse<List<TEntity>>> GetByIds(List<Guid> ids)
        {
            try
            {
                var entities = await _database.Set<TEntity>()
                    .Where(e => ids.Contains(e.Id) && !e.IsDeleted)
                    .ToListAsync();;

                return PersistenceResponseStatus.Success.GetSuccessResponse(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return PersistenceResponseStatus.PersistenceFailure.GetFailureResponse<PersistenceResponseStatus, List<TEntity>>(e.Message);
            }
        }

        public async Task<PersistenceResponse<GetAllResponse<TEntity>>> GetAll()
        {
            try
            {
                var all = await _database.Set<TEntity>().Where(e => !e.IsDeleted).ToListAsync();
                
                var getAllResponse = new GetAllResponse<TEntity>
                {
                    Items = all,
                    TotalCount = all.Count
                };

                return PersistenceResponseStatus.Success.GetSuccessResponse(getAllResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return PersistenceResponseStatus.PersistenceFailure.GetFailureResponse<PersistenceResponseStatus, GetAllResponse<TEntity>>(e.InnerException != null ? e.Message + "\n" + e.InnerException.Message : e.Message);
            }
        }
        
        public async Task<PersistenceResponse<GetAllResponse<TEntity>>> GetAll(Func<TEntity, bool> whereExpression)
        {
            try
            {
                var all = whereExpression != null
                    ? _database.Set<TEntity>().Where(e => !e.IsDeleted).AsEnumerable().Where(whereExpression).ToList()
                    : await _database.Set<TEntity>().Where(e => !e.IsDeleted).ToListAsync();
                
                var getAllResponse = new GetAllResponse<TEntity>
                {
                    Items = all,
                    TotalCount = all.Count
                };

                return PersistenceResponseStatus.Success.GetSuccessResponse(getAllResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return PersistenceResponseStatus.PersistenceFailure.GetFailureResponse<PersistenceResponseStatus, GetAllResponse<TEntity>>(e.Message);
            }
        }

        public async Task<PersistenceResponse<List<Guid>>> GetAllIds(Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                var allIds = await _database.Set<TEntity>()
                    .Where(e => !e.IsDeleted)
                    .Where(whereExpression)
                    .Select(e => e.Id)
                    .ToListAsync();

                return PersistenceResponseStatus.Success.GetSuccessResponse(allIds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return PersistenceResponseStatus.PersistenceFailure.GetFailureResponse<PersistenceResponseStatus, List<Guid>>(e.Message);
            }
        }

        public async Task<PersistenceResponse<TEntity>> First(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var first = await _database.Set<TEntity>().FirstAsync(predicate);
                return PersistenceResponseStatus.Success.GetSuccessResponse(first);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return PersistenceResponseStatus.PersistenceFailure.GetFailureResponse<PersistenceResponseStatus, TEntity>();
            }
        }
    }
    
    public interface IRepository<TEntity> :  IManipulationPersistenceGateway<TEntity> where TEntity : FullAuditedEntity
    {
    }

    public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity> 
        where TEntity : FullAuditedEntity, new()
    {

        private readonly DbContext _database;
        public Repository(ICurrentDbContext database) : base(database)
        {
            _database = database.Context;
        }
        

        public async Task<PersistenceResponse<TEntity>> Create(TEntity entity)
        {
            try
            {
                entity.CreationTime = DateTime.Now;
                // TODO: CREATOR ID
                entity.Id = Guid.NewGuid();
                await _database.AddAsync(entity);
                await _database.SaveChangesAsync();

                return PersistenceResponseStatus.Success.GetSuccessResponse(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return PersistenceResponseStatus.PersistenceFailure.GetFailureResponse<PersistenceResponseStatus, TEntity>(e.InnerException != null ? e.Message + "\n" + e.InnerException.Message : e.Message);
            }
        }

        public async Task<PersistenceResponse<TEntity>> Update(TEntity entity)
        {
            try
            {
                entity.LastModificationTime = DateTime.Now;
                
                var entityToUpdate = await _database.FindAsync<TEntity>(entity.Id);
                if (entityToUpdate.IsDeleted) return PersistenceResponseStatus.PersistenceFailure.GetFailureResponse<PersistenceResponseStatus, TEntity>("AlreadyWasDeleted");
                _database.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _database.SaveChangesAsync();

                return PersistenceResponseStatus.Success.GetSuccessResponse(entityToUpdate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return PersistenceResponseStatus.PersistenceFailure.GetFailureResponse<PersistenceResponseStatus, TEntity>(e.InnerException != null ? e.Message + "\n" + e.InnerException.Message : e.Message);
            }
        }

        public async Task<PersistenceResponse<DeletionResponses>> Delete(Guid id)
        {
            try
            {
                var entity = await _database.FindAsync<TEntity>(id);

                if (entity.IsDeleted)
                {
                    return new PersistenceResponse<DeletionResponses>
                    {
                        Message = "AlreadyWasDeleted",
                        Success = false,
                        Response = DeletionResponses.DeletionFailure,
                        ErrorCodeName = DeletionResponses.DeletionFailure.ToString()
                    };
                }

                
                entity.DeletionTime = DateTime.Now;
                entity.IsDeleted = true;
                await _database.SaveChangesAsync();

                return new PersistenceResponse<DeletionResponses>
                {
                    Message = "Success",
                    Success = true,
                    Response = DeletionResponses.DeleteSuccessfully,
                    ErrorCodeName = DeletionResponses.DeleteSuccessfully.ToString()
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new PersistenceResponse<DeletionResponses>
                {
                    Message = e.Message,
                    Success = false,
                    Response = DeletionResponses.DeletionFailure,
                    ErrorCodeName = DeletionResponses.DeletionFailure.ToString()
                };
            }
        }
    }
}