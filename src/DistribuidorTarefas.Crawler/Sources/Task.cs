using System;

namespace DistribuidorTarefas.Crawler.Core.Sources
{
    public class Task
    {
        public Guid TaskId { get; protected set; } = Guid.NewGuid();

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string JsonConfig { get; protected set; }

        public string JsonResult { get; protected set; }

        public string CallbackUrl { get; protected set; }

        public int ExecutionCounter { get; protected set; }

        public int CallbackCounter { get; protected set; }

        public string Receiver { get; protected set; }

        public int InsuranceId { get; protected set; }

        public string InsuranceName { get; protected set; }

        public int ProcessId { get; protected set; }

        public string ProcessName { get; protected set; }

        public int TaskStatus { get; protected set; } = 1;

        public DateTime CreateDate { get; protected set; } = DateTime.Now;

        public DateTime? StartedDate { get; protected set; }

        public DateTime? FinishedDate { get; protected set; }


        protected Task()
        {

        }

        public Task(
            string name,
            string description,
            string jsonConfig,
            string callbackUrl,
            string receiver,
            int insuranceId,
            string insuranceName,
            int processId,
            string processName)
        {
            Name = name;
            Description = description;
            JsonConfig = jsonConfig;
            CallbackUrl = callbackUrl;
            Receiver = receiver;
            InsuranceId = insuranceId;
            InsuranceName = insuranceName;
            ProcessId = processId;
            ProcessName = processName;
        }
    }
}
