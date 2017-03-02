namespace DistribuidorTarefas.Crawler.Core.Sources
{
    public class ArquivoResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public DadosRetorno Data { get; set; }
    }
}
