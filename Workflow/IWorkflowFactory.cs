using System;

namespace Workflow
{
    public interface IWorkflowFactory
    {
        IWorkflow CreateWorkflow(Action<IWorkflowStepCollectionBuilder> action);
    }
}