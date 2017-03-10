using Minuto.Core.Utils.Transactions;
using Minuto.Core.Utils.Data;
using Minuto.Core.Types;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace DistribuidorTarefas.Crawler.Core.Queries
{
    public class GetChildStepsTaskConfigStep
    {
        private readonly DataAccess DataAccess = DataAccess.NewInstance(TraceableID.NewTraceID(), new DbContext("Crawler.Task"));

        public GetChildStepsTaskConfigStep()
        { }

        public List<Guid> Execute(decimal taskConfigStepId)
        {
            return MinutoTransaction
                .FromReadUncommitedScope(transacao =>
                    DataAccess
                        .SqlQuery<Guid>($"EXEC dbo.sp_PVD_GetChildStepsTaskConfigStep {taskConfigStepId}")
                        .ToList(),
                    true);
        }
    }
}

