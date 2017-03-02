using System;

namespace DistribuidorTarefas.Crawler.WebApi.Infrastructure.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message)
            : base(message)
        { }
    }
}