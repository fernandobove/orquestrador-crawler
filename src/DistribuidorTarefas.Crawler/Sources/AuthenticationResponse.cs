using System;

namespace DistribuidorTarefas.Crawler.Core.Sources
{
    public class AuthenticationResponse
    {
        /// <summary>
        /// Usuário do APP HUB
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Token retornado do APP HUB
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Data de expiração do token
        /// </summary>
        public DateTime ExpirationDate { get; set; }

    }
}
