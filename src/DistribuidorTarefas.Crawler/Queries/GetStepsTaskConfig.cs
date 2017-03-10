using DistribuidorTarefas.Crawler.Core.Sources;
using Minuto.Core.Utils.Transactions;
using Minuto.Core.Utils.Data;
using Minuto.Core.Types;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace DistribuidorTarefas.Crawler.Core.Queries
{
    public class GetStepsTaskConfig
    {
        private readonly DataAccess DataAccess = DataAccess.NewInstance(TraceableID.NewTraceID(), new DbContext("Crawler.Task"));

        public GetStepsTaskConfig()
        { }

        public List<StepConfig> Execute(Guid taskConfigId)
        {
            return MinutoTransaction
                .FromReadUncommitedScope(transacao =>
                    DataAccess
                        .SqlQuery<StepConfig>($"EXEC dbo.sp_PVD_GetStepsTaskConfig '{taskConfigId}'")
                        .ToList(),
                    true);
        }
    }
}

