using System;
using DistribuidorTarefas.Crawler.Core.Applications;

namespace DistribuidorTarefas.Crawler.Worker
{
    public class Program
    {
        private static DistribuidorTarefasApoliceBradescoApplication _distribuidorTarefasApoliceBradescoApplication = new DistribuidorTarefasApoliceBradescoApplication();
        private static AuthenticationAppHubApplication _authenticationAppHubApplication = new AuthenticationAppHubApplication();
        private static UploadFileAppHubApplication _uploadFileAppHubApplication = new UploadFileAppHubApplication();

        static void Main(string[] args)
        {
            var auth_response = _authenticationAppHubApplication.Execute();

            Console.WriteLine(auth_response.AccessToken);
            Console.WriteLine(auth_response.ExpirationDate);
            Console.WriteLine(auth_response.UserName);

            var opcao = string.Empty;
            var file_path = string.Empty;

            while (opcao.Trim().ToLower() != "sair")
            {
                Console.Write("Opção (arquivo upload/sair): ");
                opcao = Console.ReadLine();

                if (opcao.Trim().ToLower() == "sair")
                {
                    break;
                }

                //_distribuidorTarefasApoliceBradescoApplication.Execute();

                _uploadFileAppHubApplication.Execute(auth_response, opcao);
            }

        }
    }
}

