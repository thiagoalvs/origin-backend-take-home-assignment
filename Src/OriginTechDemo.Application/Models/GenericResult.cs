using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Application.Models
{
    public class GenericResult
    {
        public HttpStatusCode StatusCode { get; set; }

        public object Data { get; set; }

        public GenericResult(HttpStatusCode statusCode, List<ValidationFailure> validationFailures)
        {
            StatusCode = statusCode;
            Data = validationFailures.Select(error => $"Property: {error.PropertyName} - Reason: {error.ErrorMessage}").ToList();
        }

        public GenericResult(HttpStatusCode statusCode, string validationFailure)
        {
            StatusCode = statusCode;
            Data = new List<string> { validationFailure };
        }

        public GenericResult(HttpStatusCode statusCode, object data)
        {
            StatusCode = statusCode;
            Data = data;
        }
    }
}
