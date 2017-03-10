using System;
using System.Collections.Generic;

namespace DistribuidorTarefas.Crawler.Core.Sources
{
    public class StepConfig
    {
        public Guid StepId { get; set; }
        public string JsonStepConfig { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal TaskConfigStepId { get; set; }
        public ICollection<Guid> ChildSteps { get; set; }
    }
}
