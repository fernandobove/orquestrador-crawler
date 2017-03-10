using Crawler.Seguradoras.Contract;
using DistribuidorTarefas.Crawler.Core.Queries;
using DistribuidorTarefas.Crawler.Core.Sources;
using Newtonsoft.Json;

namespace DistribuidorTarefas.Crawler.Core.Commands
{
    public class BuildTaskConfigurationCommand
    {
        private readonly GetTaskConfiguration _getTaskConfiguration = new GetTaskConfiguration();
        private readonly GetPropertiesBindings _getPropertiesBindings = new GetPropertiesBindings();
        private readonly GetStepsTaskConfig _getStepsTaskConfig = new GetStepsTaskConfig();
        private readonly GetChildStepsTaskConfigStep _getChildStepsTaskConfigStep = new GetChildStepsTaskConfigStep();

        public BuildTaskConfigurationCommand()
        { }

        public TaskConfiguration Execute(Seguradora seguradora)
        {
            var taskConfiguration = _getTaskConfiguration.Execute(seguradora.Id, seguradora.Processo);

            var stepsConfig = _getStepsTaskConfig.Execute(taskConfiguration.TaskConfigId);

            foreach (StepConfig stepConfig in stepsConfig)
            {
                stepConfig.ChildSteps = _getChildStepsTaskConfigStep.Execute(stepConfig.TaskConfigStepId);

                if (!string.IsNullOrEmpty(stepConfig.UserName))
                {
                    stepConfig.JsonStepConfig = stepConfig.JsonStepConfig.Replace("{{UserName}}", stepConfig.UserName);
                }

                if (!string.IsNullOrEmpty(stepConfig.Password))
                {
                    stepConfig.JsonStepConfig = stepConfig.JsonStepConfig.Replace("{{Password}}", stepConfig.Password);
                }

                var step = JsonConvert.DeserializeObject<Step>(stepConfig.JsonStepConfig);

                step.ChildSteps = stepConfig.ChildSteps;

                taskConfiguration.Steps.Add(step);
            }

            taskConfiguration.PropertiesBindings = _getPropertiesBindings.Execute(taskConfiguration.TaskConfigId);

            return taskConfiguration;
        }
    }
}
