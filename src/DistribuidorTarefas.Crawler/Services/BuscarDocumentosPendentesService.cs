using DistribuidorTarefas.Crawler.Core.Sources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace DistribuidorTarefas.Crawler.Core.Services
{
    public class BuscarDocumentosPendentesService
    {
        private string _docsPendentesUrl;

        public BuscarDocumentosPendentesService()
        {
            _docsPendentesUrl = ConfigurationManager.AppSettings["DocsPendentes.Url"];
        }

        public Documento ObterDocumentosPendentes(int seguradoraId, int tipoDocumentoId, int page, int pageSize)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = client
                    .GetAsync(_docsPendentesUrl + seguradoraId.ToString() + "?tipodocumento=" + tipoDocumentoId + "&page=" + page + "&pagesize=" + pageSize)
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result;

                var documento =  JsonConvert.DeserializeObject<Documento>(result);

                return documento;
            }
        }
    }
}
