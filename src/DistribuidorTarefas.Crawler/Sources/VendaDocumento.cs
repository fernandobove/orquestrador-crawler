using System;

namespace DistribuidorTarefas.Crawler.Core.Sources
{
    public class VendaDocumento
    {
        /// <summary>
        /// Código interno pós-vendas gerado no cadastro do documento
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// Código da venda
        /// </summary>
        public Int64 VendaId { get; set; }

        /// <summary>
        /// Encontrado no domínio de Tipo de Documento
        /// </summary>
        public Int16 TipoDocumentoId { get; set; }

        /// <summary>
        /// Chave vindo do APP Hub após o documento ser cadastrado
        /// </summary>
        public string ApplicationHubId { get; set; }

        /// <summary>
        /// Informação do aws caso venha preenchido
        /// </summary>
        public string UrlAppHub { get; set; }

        /// <summary>
        /// Preenchido no momento do cadastro
        /// </summary>
        public DateTime DataCriacao { get; set; }
    }
}
