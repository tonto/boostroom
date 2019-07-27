using System.Net;
using BoostRoom.Accounts.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BoostRoom.WebApp.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public CustomExceptionFilterAttribute(
            IHostingEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public override void OnException(ExceptionContext context)
        {
//            if (!_hostingEnvironment.IsDevelopment())
//            {
//                return;
//            }

            // TODO - If not domain exception and isDev, throw

            var statusCode = (int) HttpStatusCode.InternalServerError;

            if (context.Exception is DomainException) statusCode = (int) HttpStatusCode.BadRequest;

            var ex = new CustomJsonExceptionResponse
            {
                ErrorMessage = context.Exception.Message,
                Exception = context.Exception.GetType().Name,
                Status = statusCode,
            };

            var result = new JsonResult(ex)
            {
                StatusCode = statusCode 
            };

            context.Result = result;
        }
    }
}