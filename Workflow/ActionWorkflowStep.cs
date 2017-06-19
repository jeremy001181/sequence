using System;
using System.Threading.Tasks;

namespace Workflow
{
    internal class ActionWorkflowStep : WorkflowStep
    {
        private readonly Action<IWorkflowContext, Func<IWorkflowContext, Task>> _action;

        internal ActionWorkflowStep(Action<IWorkflowContext, Func<IWorkflowContext, Task>> action)
        {
            _action = action;
        }

        public override async Task RunAsync(IWorkflowContext context)
        {
            _action(context, async workflowContext =>
            {
                await Next(workflowContext);
            });

            await Task.FromResult(0);
        }
    }
}