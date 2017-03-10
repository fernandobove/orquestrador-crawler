using System;
using DistribuidorTarefas.Crawler.Core.Applications;
using DistribuidorTarefas.Crawler.Core.Sources;

namespace DistribuidorTarefas.Crawler.Worker
{
    public class Program
    {
        private static ExecutarRoboBaixarDocumentoApplication _executarRoboBaixarDocumentoApplication = new ExecutarRoboBaixarDocumentoApplication();
        private static ExecutarRoboBaixarDocumentoOGMApplication _executarRoboBaixarDocumentoOGMApplication = new ExecutarRoboBaixarDocumentoOGMApplication();

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    var sistema = args[0];

                    var seguradora = new Seguradora { Id = int.Parse(args[1]), Nome = args[2], Processo = (int)Enum.Parse(typeof(Processo), args[3]) };

                    var opcao = string.Empty;

                    while (opcao.Trim().ToLower() != "sim")
                    {
                        if (sistema == Sistema.OGM.ToString())
                        {
                            _executarRoboBaixarDocumentoOGMApplication.Execute(seguradora);
                        }
                        else
                        {
                            _executarRoboBaixarDocumentoApplication.Execute(seguradora);
                        }

                        Console.Write("Deseja sair? (sim/não): ");
                        opcao = Console.ReadLine();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Read();
                }
            }
        }
    }
}

