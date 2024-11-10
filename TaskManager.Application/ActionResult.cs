namespace TaskManager.Application
{
    public class ActionResult
    {
        public static ActionResult Create(bool sucess, string message, object content)
        {
            return new ActionResult
            {
                Content = content,
                Message = message,
                Success = sucess
            };
        }

        public virtual object? Content { get; private set; }
        public virtual bool Success { get; private set; }
        public virtual string Message { get; private set; }
    }
}
