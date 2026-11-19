using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Exceptions
{
    public abstract class AppExceptions(string message, int statusCode, Exception? innerException = null) : Exception(message,innerException) 
    {
        public int StatusCode { get; } = statusCode;
    }

    public class NotFoundException(string message) : AppExceptions(message: message, statusCode: 404);
    public class ForbiddenException(string message) : AppExceptions(message, 403);
    public class ConflictException(string message) : AppExceptions(message, 409);



}
