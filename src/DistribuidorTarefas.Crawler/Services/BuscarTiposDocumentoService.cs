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
    public class BuscarTiposDocumentoService
    {
        private readonly string _tipoDocumentoUrl;

        public BuscarTiposDocumentoService()
        {
            _tipoDocumentoUrl = ConfigurationManager.AppSettings["TipoDocumento.Url"];
        }

        public List<TipoDocumento> ObterListaTipoDocumento()
        {
            using (HttpClient client = new HttpClient())
            {
                var result = client
                    .GetAsync(_tipoDocumentoUrl)
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result;

                return JsonConvert.DeserializeObject<List<TipoDocumento>>(result);
            }
        }
    }
}
