using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Validation
{
    public class BaseValidator : IValidator
    {
        private Dictionary<string, string> _modelState;

        public BaseValidator()
        {
            _modelState = new Dictionary<string, string>();
        }
        public bool IsValid => !_modelState.Any();

        public void AddError(string key, string errorMessage)
        {
            _modelState.Add(key, errorMessage);
        }

        public Dictionary<string, string> GetErrors()
        {
            return _modelState;
        }
    }
}
