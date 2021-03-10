using System;
using System.Collections.Generic;
using Kwetter.Services.Core.Api.Filters;
using Kwetter.Services.Core.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kwetter.Services.Tweet.Api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), BaseApiExceptionHandlers.HandleValidationException },
                { typeof(NotFoundException), BaseApiExceptionHandlers.HandleNotFoundException },
                { typeof(UnauthorizedAccessException), BaseApiExceptionHandlers.HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), BaseApiExceptionHandlers.HandleForbiddenAccessException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                BaseApiExceptionHandlers.HandleInvalidModelStateException(context);
                return;
            }

            BaseApiExceptionHandlers.HandleUnknownException(context);
        }
    }
}
