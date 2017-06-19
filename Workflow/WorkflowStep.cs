using System.Threading.Tasks;

namespace Workflow
{
    public abstract class WorkflowStep
    {
        public abstract Task RunAsync(IWorkflowContext context);

        protected async Task Next(IWorkflowContext context)
        {
            if (NextStep != null)
            {
                await NextStep.RunAsync(context);
            }
        }

        internal WorkflowStep NextStep { get; set; }
    }
}
