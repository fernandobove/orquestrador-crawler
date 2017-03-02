using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistribuidorTarefas.Crawler.Core.Sources
{
    public class ArquivoRequest
    {
        public string Name { get; set; }
        public string MimeType { get; set; }
        public string CreatedBy { get; set; }
        public string ClientKey { get; set; }
        public byte[] Content { get; set; }
        public CategoriaRequest Category { get; set; }
        public AplicacaoRequest Application { get; set; }
    }
}
