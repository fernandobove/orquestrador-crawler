using Crawler.Seguradoras.Contract;
using DistribuidorTarefas.Crawler.Core.Services;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Configuration;
using DistribuidorTarefas.Crawler.Core.Sources;
using System.Collections.Generic;
using DistribuidorTarefas.Crawler.Core.Commands;

namespace DistribuidorTarefas.Crawler.Core.Applications
{
    public class ExecutarRoboBaixarDocumentoApplication
    {
        private readonly BuscarDocumentosPendentesService _buscarDocumentosPendentesService = new BuscarDocumentosPendentesService();
        private readonly BuildTaskConfigurationCommand _buildTaskConfigurationCommand = new BuildTaskConfigurationCommand();
        private readonly string _callbackUrl = ConfigurationManager.AppSettings["Callback.Url"];
        private readonly string _crawlerUrl = ConfigurationManager.AppSettings["Crawler.Url"];
        private readonly int _pageSize = int.Parse(ConfigurationManager.AppSettings["DocsPendentes.PageSize"]);

        public ExecutarRoboBaixarDocumentoApplication()
        { }

        public void Execute(Seguradora seguradora)
        {
            var documento = _buscarDocumentosPendentesService.ObterDocumentosPendentes(seguradora.Id, seguradora.Processo, 1, _pageSize);
            var listaPendencias = documento.Documentos;

            for (int i = 1; i * _pageSize < documento.Total; i++)
            {
                listaPendencias.AddRange(ObterListaDocumentosPendentes(seguradora, i + 1));
            }

            if (listaPendencias.Count > 0)
            {
                var config = _buildTaskConfigurationCommand.Execute(seguradora);

                config.Sources = listaPendencias.ToArray();

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

        private List<DocumentoPendente> ObterListaDocumentosPendentes(Seguradora seguradora, int page)
        {
            var documento = _buscarDocumentosPendentesService.ObterDocumentosPendentes(seguradora.Id, seguradora.Processo, page, _pageSize);

            return documento.Documentos;
        }

    }
}
