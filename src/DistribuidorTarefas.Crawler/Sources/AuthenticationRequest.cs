namespace DistribuidorTarefas.Crawler.Core.Sources
{
    public class AuthenticationRequest
    {
        /// <summary>
        /// Usuário para pedido do token
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Senha para pedido do token
        /// </summary>
        public string Password { get; set; }
    }
}
