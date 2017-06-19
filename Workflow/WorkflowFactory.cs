using System;

namespace Workflow
{
    public class WorkflowFactory : IWorkflowFactory
    {
        public IWorkflow CreateWorkflow(Action<IWorkflowStepCollectionBuilder> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var stepCollection = new WorkflowStepManager();

            action(stepCollection);

            return new Workflow(stepCollection, new WorkflowContext());
        }
    }

    public interface IWorkflowStepReader
    {
        WorkflowStep GetFirstStep();
    }
}