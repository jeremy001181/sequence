namespace Workflow
{
    using System.Threading.Tasks;

    internal class Workflow : IWorkflow
    {
        private readonly IWorkflowStepReader _stepReader;
        private readonly IWorkflowContext _context;

        internal Workflow(IWorkflowStepReader stepReader, IWorkflowContext context)
        {
            _stepReader = stepReader;
            _context = context;
        }

        public virtual async Task ExecuteAsync()
        {
            var firstStep = _stepReader.GetFirstStep();

            if(firstStep != null)await firstStep.RunAsync(_context);
        }
    }
}
