using System;

namespace Workflow
{
    public class WorkflowFactory : IWorkflowFactory
    {
        public IWorkflow CreateWorkflowAsync(Action<IWorkflowStepCollectionBuilder> action)
        {
            var stepCollection = new WorkflowStepManager();

            action(stepCollection);

            return new Workflow(stepCollection, new WorkflowContext());
        }
    }

    public class WorkflowContext : IWorkflowContext
    {
    }

    public interface IWorkflowStepReader
    {
        WorkflowStep GetFirstStep();
    }
}