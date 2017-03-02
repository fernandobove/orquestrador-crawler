using Crawler.Seguradoras.Contract.Sources;

namespace DistribuidorTarefas.Crawler.Core.Sources
{
    public class BaixarApolice : SourceBase
    {
        public int OportunidadeId { get; set; }
        public string Cpf { get; set; }
        public string Placa { get; set; }
        public string VigenciaInicio { get; set; }
        public string VigenciaTermino { get; set; }
        public string DataProposta { get; set; }
        public string NumeroOrcamento { get; set; }
        public string DiretorioDownload { get; set; }

        public string Sucursal { get; set; }
        public string Ramo { get; set; }
        public string Cia { get; set; }
        public string Apolice { get; set; }
        public string Item { get; set; }
        public string DataEmissao { get; set; }
        public string NumeroCI { get; set; }
        public string FileName { get; set; }


        public object[] Parametros { get; set; }
        public string XPath { get; set; }
        public string XPathBuilder { get; set; }
    }
}
