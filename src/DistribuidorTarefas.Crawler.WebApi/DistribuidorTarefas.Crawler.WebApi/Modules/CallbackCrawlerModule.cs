using DistribuidorTarefas.Crawler.Core.Applications;
using Nancy;
using Nancy.ModelBinding;

namespace DistribuidorTarefas.Crawler.WebApi.Modules
{
    public class CallbackCrawlerModule : BaseModule
    {
        private readonly UploadFileAppHubApplication _uploadFileAppHubApplication;

        public CallbackCrawlerModule(
            UploadFileAppHubApplication uploadFileAppHubApplication)
        {
            _uploadFileAppHubApplication = uploadFileAppHubApplication;

            //Get["transmissao/{path}"] = parameters => Get(parameters.path);
        }

        //private Response Get(string path)
        //{
        //    return Response.AsJson(_uploadFileAppHubApplication.Execute());
        //}

    }
}
