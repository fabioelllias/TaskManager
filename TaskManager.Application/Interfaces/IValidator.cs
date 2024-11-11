namespace TaskManager.Application.Interfaces
{
    public interface IValidator
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
        Dictionary<string, string> GetErrors();
    }
}
