using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Exceptions;
using TodoHub.Core.Responses;

namespace TodoHub.Services.Rules
{
    public static class ExceptionHandler<T>
    {
        public static ReturnModel<T> HandleException(Exception ex)
        {
            if (ex.GetType() == typeof(NotFoundException))
            {
                return new ReturnModel<T>()
                {
                    Message = ex.Message,
                    Success = false,
                    StatusCode =404
                };
            }
            if (ex.GetType() == typeof(ValidationException))
            {
                return new ReturnModel<T>()
                {
                    Message = ex.Message,
                    Success = false,
                    StatusCode = 400
                };
            }
            return new ReturnModel<T>()
            {
                Message = ex.Message,
                Success = false,
                StatusCode = 500
            };
        }
    }
}
