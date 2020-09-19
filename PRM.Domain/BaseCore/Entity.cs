using System;
using System.ComponentModel.DataAnnotations;

namespace PRM.Domain.BaseCore
{
    public interface IEntity
    {
        Guid Id { get; }
        string Name { get; }
        string Code { get; }
        bool IsIdEquals(Guid id);
        bool IsCodeEquals(string code);
        bool IsNameEquals(string name);
        bool NameContains(string value);
        void SetName(string name);
        void SetCode(string code);
    }
    public abstract class Entity : IEntity
    {
        #region Properties
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        #endregion

        #region Constructors
        protected Entity() {}
        public Entity(Guid id, string name, string code)
        {
            Id = id;
            SetName(name);
            SetCode(code);
        }
        #endregion

        #region Methods
        
        public bool IsIdEquals(Guid id) => Id == id;
        public bool IsCodeEquals(string code) => Code == code;
        public bool IsNameEquals(string name) => Name == name;
        public bool NameContains(string value) => Name.Contains(value);
        
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ValidationException("Name is required!");
            Name = name;
        }

        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ValidationException("Code is required!");
            Code = code;
        }
        
        #endregion
        
    }
}