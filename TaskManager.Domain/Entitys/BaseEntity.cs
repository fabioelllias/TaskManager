using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Shared.Interfaces;

namespace TaskManager.Domain.Entitys
{
    public class BaseEntity : IValidator
    {
        private readonly BaseEntity _baseEntity = new BaseEntity();
        public int Id { get; private set; }

        [NotMapped]
        public bool IsValid => _baseEntity.IsValid;

        public void AddError(string key, string errorMessage)
        {
            _baseEntity.AddError(key, errorMessage);
        }

        public Dictionary<string, string> GetErrors()
        {
            return _baseEntity.GetErrors();
        }
    }
}
