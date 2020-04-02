using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SocialVeterinary.Api.Infrastructure
{
    public class ExceptionHandlerMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _request;

        public ExceptionHandlerMiddleware(
            RequestDelegate next)
        {
            _request = next;
        }

        public Task Invoke(HttpContext context) => InvokeAsync(context);

        private async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception exception)
            {
                if (exception is ValidationException validationException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = JsonContentType;

                    await context.Response.WriteAsync(
                        JsonConvert.SerializeObject(
                            new
                                {
                                    message = validationException.Message,
                                    errors = validationException.Errors.Select(
                                        x => new { errorCode = x.ErrorCode, errorMessage = x.ErrorMessage })
                                },
                            new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore,
                                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                                }));
                }
            }
        }

    }
}
