using Crawler.Seguradoras.Contract;
using DistribuidorTarefas.Crawler.Core.Queries;
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
    public class DistribuidorTarefasApoliceBradescoApplication
    {
        private readonly GetListaApolicesPendentes _getListaApolicesPendentes = new GetListaApolicesPendentes();
        private readonly PosVendaTransmissaoService _posVendaTransmissaoService = new PosVendaTransmissaoService();
        private readonly string _callbackUrl = ConfigurationManager.AppSettings["Callback.Url"];
        private readonly string _crawlerUrl = ConfigurationManager.AppSettings["Crawler.Url"];

        private readonly int _seguradoraBradescoId = 4;

        public DistribuidorTarefasApoliceBradescoApplication()
        { }

        public void Execute()
        {
            var listaTipoDocumento = _posVendaTransmissaoService.ObterListaTipoDocumento();

            var listaPendencias = _getListaApolicesPendentes.Execute(_seguradoraBradescoId).ToArray();

            if (listaPendencias.Length > 0)
            {
                string jsonConfiguration = File.ReadAllText(ConfigurationManager.AppSettings["JSON.Default.Directory"]);

                var config = JsonConvert.DeserializeObject<TaskConfiguration>(jsonConfiguration);

                config.Sources = listaPendencias;

                jsonConfiguration = JsonConvert.SerializeObject(config);

                Task task = new Task("Bradesco", "Piloto Apólice", jsonConfiguration, _callbackUrl, $"{Environment.MachineName}:9001", 4, "Bradesco", 99, "Bradesco - Piloto Apólice");

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

                Console.WriteLine("Criando Tarefa");
            }
        }

    }
}
