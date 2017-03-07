using System;
using Crawler.Seguradoras.Contract.Sources;
using Newtonsoft.Json;

namespace DistribuidorTarefas.Crawler.Core.Sources
{
    public class DocumentoPendente : SourceBase
    {
        public long VendaId { get; set; }
        public string Cpf { get; set; }
        public int SolicitacaoMinutoId { get; set; }
        public int OportunidadeId { get; set; }
        public string Placa { get; set; }
        public string NumeroOrcamento { get; set; }
        public string DataTransmissao { get; set; }
        public string VigenciaInicio { get; set; }
        public string VigenciaTermino { get; set; }
        public string DataProposta { get; set; }
        public string DiretorioDownload { get; set; }
    }

}
