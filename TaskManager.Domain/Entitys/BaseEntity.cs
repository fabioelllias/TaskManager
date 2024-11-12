using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Shared.Interfaces;

namespace TaskManager.Domain.Entitys
{
    public class BaseEntity : IValidator
    {
        private Dictionary<string, string> _errors = new();
        public int Id { get; private set; }
        public bool IsNew() { return Id <= 0; }

        [NotMapped]
        public bool IsValid => !_errors.Any();

        public void AddError(string key, string errorMessage)
        {
            _errors.Add(key, errorMessage);
        }

        public Dictionary<string, string> GetErrors()
        {
            return _errors;
        }

      
        internal void AddError(Dictionary<string, string> dictionary)
        {
            foreach (var error in dictionary)
                AddError(error.Key, error.Value);
        }
    }
}
