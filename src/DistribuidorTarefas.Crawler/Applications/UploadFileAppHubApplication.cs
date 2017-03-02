using System;
using System.IO;
using System.Configuration;
using DistribuidorTarefas.Crawler.Core.Sources;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace DistribuidorTarefas.Crawler.Core.Applications
{
    public class UploadFileAppHubApplication
    {
        private static string _appHubUrl = ConfigurationManager.AppSettings["AppHubUpload.Url"];
        private static string _appRequestId = ConfigurationManager.AppSettings["AppRequestId"];

        public UploadFileAppHubApplication()
        { }

        public void Execute(AuthenticationResponse auth_response, string file_path)
        {
            var my_file = File.ReadAllBytes(file_path);

            var app_file_request = new ArquivoRequest
            {
                Application = new AplicacaoRequest() { Id = _appRequestId },
                Category = new CategoriaRequest() { Id = 10 }, // --->> Proposta
                ClientKey = Guid.NewGuid().ToString(),
                Content = my_file,
                CreatedBy = "DistribuidorTarefas.Crawler",
                MimeType = "application/PDF",
                Name = "CAP.CTDIEBRA.0969000566880001.D161121.PDF"
            };

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(app_file_request, Formatting.None), Encoding.UTF8, "application/json");

                content.Headers.Add("Bearer", auth_response.AccessToken);

                var result = client
                    .PostAsync(_appHubUrl, content)
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result;

                var arquivoResponse = JsonConvert.DeserializeObject<ArquivoResponse>(result);

                Console.WriteLine(arquivoResponse.Code);
                Console.WriteLine(arquivoResponse.Data.ApplicationKey);
            }
        }
    }
}
