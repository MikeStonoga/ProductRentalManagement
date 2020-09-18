using System;

namespace PRM.Domain.BaseCore
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public abstract class FullAuditedEntity : Entity
    {
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}