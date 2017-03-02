using DistribuidorTarefas.Crawler.Core.Sources;
using DistribuidorTarefas.Crawler.Data.Context;
using Minuto.Core.Utils.Transactions;
using Minuto.Core.Utils.Data;
using Minuto.Core.Types;
using System.Collections.Generic;
using System.Linq;

namespace DistribuidorTarefas.Crawler.Core.Queries
{
    public class GetListaApolicesPendentes
    {
        private readonly DataAccess DataAccess = DataAccess.NewInstance(TraceableID.NewTraceID(), new DataContext());

        public GetListaApolicesPendentes()
        { }

        public List<BaixarApolice> Execute(int seguradoraId)
        {
            return MinutoTransaction
                .FromReadUncommitedScope(transacao =>
                    DataAccess
                        .SqlQuery<BaixarApolice>($"EXEC dbo.sp_PVD_ListarOportunidadesVendidasSemApolice {seguradoraId}")
                        .ToList(),
                    true);
        }
    }
}

