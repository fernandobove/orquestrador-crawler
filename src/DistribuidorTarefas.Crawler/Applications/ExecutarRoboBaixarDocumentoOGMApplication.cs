using Crawler.Seguradoras.Contract;
using DistribuidorTarefas.Crawler.Core.Queries;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Configuration;
using DistribuidorTarefas.Crawler.Core.Sources;
using DistribuidorTarefas.Crawler.Core.Commands;

namespace DistribuidorTarefas.Crawler.Core.Applications
{
    public class ExecutarRoboBaixarDocumentoOGMApplication
    {
        private readonly GetListaApolicesPendentes _getListaApolicesPendentes = new GetListaApolicesPendentes();
        private readonly BuildTaskConfigurationCommand _buildTaskConfigurationCommand = new BuildTaskConfigurationCommand();
        private readonly string _callbackUrl = ConfigurationManager.AppSettings["CallbackOGM.Url"];
        private readonly string _crawlerUrl = ConfigurationManager.AppSettings["Crawler.Url"];

        public ExecutarRoboBaixarDocumentoOGMApplication()
        { }

        public void Execute(Seguradora seguradora)
        {
            var listaPendencias = _getListaApolicesPendentes.Execute(seguradora.Id).ToArray();

            if (listaPendencias.Length > 0)
            {
                var config = _buildTaskConfigurationCommand.Execute(seguradora);

                config.Sources = listaPendencias;

                string jsonConfiguration = JsonConvert.SerializeObject(config);

                var descricaoProcesso = Enum.GetName(typeof(Processo), seguradora.Processo);

                Task task = new Task(seguradora.Nome, descricaoProcesso, jsonConfiguration, _callbackUrl, $"{Environment.MachineName}:9001", seguradora.Id, seguradora.Nome, seguradora.Processo, $"{seguradora.Nome} - {descricaoProcesso}");

                //TODO: Remover print de linha na versão de produção
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

                    //TODO: Remover print de linha na versão de produção
                    Console.WriteLine(result);
                }
            }
        }

    }
}
