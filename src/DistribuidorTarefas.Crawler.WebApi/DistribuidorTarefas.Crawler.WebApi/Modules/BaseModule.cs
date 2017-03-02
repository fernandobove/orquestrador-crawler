using Nancy;
using DistribuidorTarefas.Crawler.WebApi.Lib.Handlers;

namespace DistribuidorTarefas.Crawler.WebApi.Modules
{
    public abstract class BaseModule : NancyModule
    {
        protected BaseModule()
        {
            OnError += new ErrorHandler().OnError;
        }

        protected BaseModule(string modulePath)
            : base(modulePath)
        {
            OnError += new ErrorHandler().OnError;
        }
    }
}