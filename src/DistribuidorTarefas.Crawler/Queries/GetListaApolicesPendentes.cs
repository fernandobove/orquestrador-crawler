using DistribuidorTarefas.Crawler.Core.Sources;
using Minuto.Core.Utils.Transactions;
using Minuto.Core.Utils.Data;
using Minuto.Core.Types;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DistribuidorTarefas.Crawler.Core.Queries
{
    public class GetListaApolicesPendentes
    {
        private readonly DataAccess DataAccess = DataAccess.NewInstance(TraceableID.NewTraceID(), new DbContext("Minuto.Web"));

        public GetListaApolicesPendentes()
        { }

        public List<DocumentoPendente> Execute(int seguradoraId)
        {
            return MinutoTransaction
                .FromReadUncommitedScope(transacao =>
                    DataAccess
                        .SqlQuery<DocumentoPendente>($"EXEC dbo.sp_PVD_ListarOportunidadesVendidasSemApolice {seguradoraId}")
                        .ToList(),
                    true);
        }
    }
}

