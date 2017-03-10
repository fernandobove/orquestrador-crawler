using Minuto.Core.Utils.Transactions;
using Minuto.Core.Utils.Data;
using Minuto.Core.Types;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;
using Crawler.Seguradoras.Contract.Sources;

namespace DistribuidorTarefas.Crawler.Core.Queries
{
    public class GetPropertiesBindings
    {
        private readonly DataAccess DataAccess = DataAccess.NewInstance(TraceableID.NewTraceID(), new DbContext("Crawler.Task"));

        public GetPropertiesBindings()
        { }

        public List<OutputPropertyBinding> Execute(Guid taskConfigId)
        {
            return MinutoTransaction
                .FromReadUncommitedScope(transacao =>
                    DataAccess
                        .SqlQuery<OutputPropertyBinding>($"EXEC dbo.sp_PVD_GetPropertiesBindings '{taskConfigId}'")
                        .ToList(),
                    true);
        }
    }
}

