using Crawler.Seguradoras.Contract;
using DistribuidorTarefas.Crawler.Core.Services;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Configuration;
using DistribuidorTarefas.Crawler.Core.Sources;

namespace DistribuidorTarefas.Crawler.Core.Applications
{
    public class ExecutarRoboBaixarDocumentoApplication
    {
        private readonly BuscarDocumentosPendentesService _buscarDocumentosPendentesService = new BuscarDocumentosPendentesService();
        private readonly string _callbackUrl = ConfigurationManager.AppSettings["Callback.Url"];
        private readonly string _crawlerUrl = ConfigurationManager.AppSettings["Crawler.Url"];
        private readonly int _pageSize = int.Parse(ConfigurationManager.AppSettings["DocsPendentes.PageSize"]);

        public ExecutarRoboBaixarDocumentoApplication()
        { }

        public void Execute(Seguradora seguradora)
        {
            var documento = _buscarDocumentosPendentesService.ObterDocumentosPendentes(seguradora.Id, seguradora.Processo, 1, _pageSize);
            var listaPendencias = documento.Documentos;

            for (int i=1; i*_pageSize < documento.Total; i++)
            {
                documento = _buscarDocumentosPendentesService.ObterDocumentosPendentes(seguradora.Id, seguradora.Processo, i+1, _pageSize);
                listaPendencias.AddRange(documento.Documentos);
            }
            
            if (listaPendencias.Count > 0)
            {
                string jsonConfiguration = File.ReadAllText(JsonConfigurationPath(seguradora.Nome));

                var config = JsonConvert.DeserializeObject<TaskConfiguration>(jsonConfiguration);

                config.Sources = listaPendencias.ToArray();

                jsonConfiguration = JsonConvert.SerializeObject(config);

                var descricaoProcesso = Enum.GetName(typeof(Processo), seguradora.Processo);

                Task task = new Task(seguradora.Nome, descricaoProcesso, jsonConfiguration, _callbackUrl, $"{Environment.MachineName}:9001", seguradora.Id, seguradora.Nome, seguradora.Processo, $"{seguradora.Nome} - {descricaoProcesso}");

                Console.WriteLine(JsonConvert.SerializeObject(task));

                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(task, Formatting.None), Encoding.UTF8, "application/json");

                    var result = client
                        .PostAsync(_crawlerUrl, content)
                        .Result
                        .Content
                        .ReadAsStringAsync()
                        .Result;


                    Console.WriteLine(result);
                }
            }
        }

        private string JsonConfigurationPath(string seguradora)
        {
            return ConfigurationManager.AppSettings["JSON.Default.Directory"].Replace("seguradora", seguradora.ToLower());
        }

    }
}
