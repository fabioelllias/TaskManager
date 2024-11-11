using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace TaskManager.API.Filters
{
    public class ResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as Microsoft.AspNetCore.Mvc.ObjectResult;

            if (result != null)
            {
                var responseObject = result.Value as TaskManager.Application.ActionResult;

                if (responseObject != null)
                {
                    if (!responseObject.Success)
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.HttpContext.Response.ContentType = "application/json";
                        await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(responseObject));
                        return;
                    }
                }
            }

            await next();

        }
    }
}