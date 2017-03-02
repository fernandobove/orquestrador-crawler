using System;
using System.Collections.Generic;
using System.Text;
using DistribuidorTarefas.Crawler.WebApi.Infrastructure.Exceptions;
using Nancy;
using Newtonsoft.Json;

namespace DistribuidorTarefas.Crawler.WebApi.Lib.Handlers
{
    public class ErrorHandler
    {
        private readonly IDictionary<Type, Func<Exception, dynamic>> _mappedException = new Dictionary<Type, Func<Exception, dynamic>>
        {
            {
                typeof(ResourceNotFoundException), exception => new
                {
                    Messages = new List<string> { exception.Message },
                    HttpStatusCode = HttpStatusCode.NotFound
                }
            }
        };

        public virtual Response OnError(NancyContext nancyContext, Exception exception)
        {
            var typeException = exception.GetType();

            if (!_mappedException.ContainsKey(typeException))
            {
                return CreateResponse(exception.Message, HttpStatusCode.InternalServerError);
            }

            var mappedException = _mappedException[typeException](exception);
            var errorMessages = mappedException.Messages ?? new List<string>();
            return CreateResponse(errorMessages, mappedException.HttpStatusCode);
        }

        private static Response CreateResponse(string error, HttpStatusCode httpStatusCode)
        {
            return CreateResponse(new[] { error }, httpStatusCode);
        }

        private static Response CreateResponse(IEnumerable<string> errors, HttpStatusCode httpStatusCode)
        {
            var erros = new { erros = errors };
            var jsonArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(erros));

            return new Response
            {
                StatusCode = httpStatusCode,
                ContentType = "application/json",
                Contents = stream => stream.Write(jsonArray, 0, jsonArray.Length)
            };
        }
    }
}