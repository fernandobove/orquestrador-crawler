using DistribuidorTarefas.Crawler.Core.Sources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DistribuidorTarefas.Crawler.Core.Services
{
    public class PosVendaTransmissaoService
    {
        private readonly string _transmissaoUrl;

        public PosVendaTransmissaoService()
        {
            _transmissaoUrl = ConfigurationManager.AppSettings["Transmissao.Url"];
        }

        public List<TipoDocumento> ObterListaTipoDocumento()
        {
            using (HttpClient client = new HttpClient())
            {
                var result = client
                    .GetAsync(_transmissaoUrl)
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result;

                Console.WriteLine(result);

                Console.ReadLine();

                return JsonConvert.DeserializeObject<List<TipoDocumento>>(result);
            }
        }
    }
}
