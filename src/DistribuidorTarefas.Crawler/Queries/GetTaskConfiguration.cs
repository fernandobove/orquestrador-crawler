using Crawler.Seguradoras.Contract;
using Minuto.Core.Utils.Transactions;
using Minuto.Core.Utils.Data;
using Minuto.Core.Types;
using System.Linq;
using System.Data.Entity;

namespace DistribuidorTarefas.Crawler.Core.Queries
{
    public class GetTaskConfiguration
    {
        private readonly DataAccess DataAccess = DataAccess.NewInstance(TraceableID.NewTraceID(), new DbContext("Crawler.Task"));

        public GetTaskConfiguration()
        { }

        public TaskConfiguration Execute(int insurancoCoId, int processId)
        {
            return MinutoTransaction
                .FromReadUncommitedScope(transacao =>
                    DataAccess
                        .SqlQuery<TaskConfiguration>($"EXEC dbo.sp_PVD_GetTaskConfiguration {insurancoCoId}, {processId}").FirstOrDefault(),
                    true);
        }
    }
}

