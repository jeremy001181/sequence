using System;

namespace Workflow
{
    public interface IWorkflowFactory
    {
        IWorkflow CreateWorkflowAsync(Action<IWorkflowStepCollectionBuilder> action);
    }
}