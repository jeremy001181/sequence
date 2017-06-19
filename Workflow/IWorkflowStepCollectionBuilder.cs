using System;
using System.Threading.Tasks;

namespace Workflow
{
    public interface IWorkflowStepCollectionBuilder
    {
        IWorkflowStepCollectionBuilder AddStep<T>(params object [] args) where T : WorkflowStep;
        IWorkflowStepCollectionBuilder AddStep(Action<IWorkflowContext, Func<IWorkflowContext, Task>> action);
    }
}