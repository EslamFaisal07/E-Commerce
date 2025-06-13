using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    internal class CachAttribute(int DurationInSecound = 90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            // create cach key 
            string cachKey = CreateCachKey(context.HttpContext.Request);



            // check if value with cach key is exists in cach
            ICachService cachService = context.HttpContext.RequestServices.GetRequiredService<ICachService>();
            var cachValue = await cachService.GetAsync(cachKey);

            // if exists return value from cach
            if (cachValue != null)
            {
                context.Result = new ContentResult()
                {
                    Content = cachValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK

                };
                return;
            }
            // if not exists call next action and set value in cach
          var ExcutedContext = await  next.Invoke();
            if (ExcutedContext.Result is OkObjectResult result)
            {
             await   cachService.SetAsync(cachKey , result.Value , TimeSpan.FromSeconds(DurationInSecound));

                
            }


        }

        private string CreateCachKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path +'?');
            foreach (var query in request.Query.OrderBy(q=>q.Key))
            {
                Key.Append($"{query.Key}={query.Value}&");
            }
            return Key.ToString();




        }
    }
}
