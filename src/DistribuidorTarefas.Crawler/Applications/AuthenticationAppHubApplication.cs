using Crawler.Seguradoras.Contract;
using DistribuidorTarefas.Crawler.Core.Queries;
using DistribuidorTarefas.Crawler.Core.Sources;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Configuration;

namespace DistribuidorTarefas.Crawler.Core.Applications
{
    public class AuthenticationAppHubApplication
    {
        private readonly string _appHubUrl = ConfigurationManager.AppSettings["AppHub.Url"];
        private readonly string _appHubUser = ConfigurationManager.AppSettings["AppHub.User"];
        private readonly string _appHubPassword = ConfigurationManager.AppSettings["AppHub.Password"];

        public AuthenticationAppHubApplication()
        { }

        public AuthenticationResponse Execute()
        {
            var authenticationRequest = new AuthenticationRequest
            {
                UserName = _appHubUser,
                Password = _appHubPassword
            };

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(authenticationRequest, Formatting.None), Encoding.UTF8, "application/json");

                var result = client
                    .PostAsync(_appHubUrl, content)
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result;

                return JsonConvert.DeserializeObject<AuthenticationResponse>(result);
            }
        }

    }
}
