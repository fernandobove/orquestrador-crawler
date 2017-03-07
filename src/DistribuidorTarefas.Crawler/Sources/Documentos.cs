using System.Collections.Generic;

namespace DistribuidorTarefas.Crawler.Core.Sources
{
    public class Documento
    {
        public int total { get; set; }

        public List<DocumentoPendente> Documentos { get; set; }
    }
}
