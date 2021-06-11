using System;
using System.Collections.Generic;
using Kwetter.BuildingBlocks.Abstractions.Exceptions;
using Kwetter.BuildingBlocks.CQRS.Exceptions;
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
                { typeof(ValidationException), ApiExceptionHandlers.HandleValidationException },
                { typeof(NotFoundException), ApiExceptionHandlers.HandleNotFoundException },
                { typeof(UnauthorizedAccessException), ApiExceptionHandlers.HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), ApiExceptionHandlers.HandleForbiddenAccessException },
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
                ApiExceptionHandlers.HandleInvalidModelStateException(context);
                return;
            }

            ApiExceptionHandlers.HandleUnknownException(context);
        }
    }
}
