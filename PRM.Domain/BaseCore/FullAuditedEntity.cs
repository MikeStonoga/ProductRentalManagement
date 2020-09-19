using System;
using PRM.Domain.BaseCore.ValueObjects;

namespace PRM.Domain.BaseCore
{
    
    public interface IFullAuditedEntity : IEntity
    {
        Guid CreatorId { get; }
        DateTime CreationTime { get; }
        Guid? LastModifierId { get; }
        DateTime? LastModificationTime { get; }
        Guid? DeleterId { get; }
        DateTime? DeletionTime { get; }
        bool IsDeleted { get; }
        bool WasUpdated => LastModificationTime != null;
        bool WasDeleted => DeletionTime != null;
        bool WasTheCreator(Guid id);
        bool WasCreatedOnPeriod(DateRange period);
        void AddModificationInfo(Guid modifierId);
        bool WasTheLastModifier(Guid id) => LastModifierId == id;
        bool WasLastModificationOnPeriod(DateRange period) => period.IsOnRange(LastModificationTime);
        void Delete(Guid deleterId);
        bool WasTheDeleter(Guid id) => DeleterId == id;
        bool WasDeletedOnPeriod(DateRange period) => period.IsOnRange(DeletionTime);
    }
    
    public abstract class FullAuditedEntity : Entity, IFullAuditedEntity
    {
        public Guid CreatorId { get; private set; }
        public DateTime CreationTime { get; private set; }
        public Guid? LastModifierId { get; private set; }
        public DateTime? LastModificationTime { get; private set; }
        public Guid? DeleterId { get; private set; }
        public DateTime? DeletionTime { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool WasUpdated => LastModificationTime != null;
        public bool WasDeleted => DeletionTime != null;
        
        protected FullAuditedEntity() {}
        public FullAuditedEntity(Guid id, string name, string code, Guid creatorId) : base(id, name, code)
        {
            AddCreationInfo(creatorId);
        }
        
        private void AddCreationInfo(Guid creatorId)
        {
            CreatorId = creatorId;
            CreationTime = DateTime.Now;
        }

        public bool WasTheCreator(Guid id) => CreatorId == id;
        public bool WasCreatedOnPeriod(DateRange period) => period.IsOnRange(CreationTime);
        
        public void AddModificationInfo(Guid modifierId)
        {
            LastModifierId = modifierId;
            LastModificationTime = DateTime.Now;
        }
        public bool WasTheLastModifier(Guid id) => LastModifierId == id;
        public bool WasLastModificationOnPeriod(DateRange period) => period.IsOnRange(LastModificationTime);

        public void Delete(Guid deleterId)
        {
            DeleterId = deleterId;
            DeletionTime = DateTime.Now;
            IsDeleted = true;
        }

        public bool WasTheDeleter(Guid id) => DeleterId == id;
        public bool WasDeletedOnPeriod(DateRange period) => period.IsOnRange(DeletionTime);
    }
}