﻿using Contract.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthorPublisherProject.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
                }
            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next.Invoke(context);
                }
                catch (Exception ex)
                {
                    var response = context.Response;
                    response.ContentType = "application/json";
                    switch (ex)
                    {
                        case ErrorException :
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        case KeyNotFoundException:
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        default:
                            // unhandled error
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }
                    var errorResponse = new
                    {
                        message = ex.Message,
                        statusCode = response.StatusCode
                    };
                    var errorJson = JsonSerializer.Serialize(errorResponse);
                    await response.WriteAsync(errorJson);
                    throw;
            }
        }
    }
}

