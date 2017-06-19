using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Workflow.Extensions;

namespace Workflow
{
    internal class WorkflowStepManager : IWorkflowStepCollectionBuilder, IWorkflowStepReader
    {
        private readonly IList<WorkflowStep> _workflowSteps;

        internal WorkflowStepManager()
        {
            _workflowSteps = new List<WorkflowStep>();
        }

        public IWorkflowStepCollectionBuilder AddStep<T>(params object[] args) where T : WorkflowStep
        {
            var newStep = Activator.CreateInstance(typeof(T), BindingFlags.CreateInstance, args) as WorkflowStep;

            _workflowSteps.Append(newStep);

            return this;
        }
        
        public IWorkflowStepCollectionBuilder AddStep(Action<IWorkflowContext, Func<IWorkflowContext, Task>> action)
        {
            var newStep = new ActionWorkflowStep(action);
            
            _workflowSteps.Append(newStep);

            return this;
        }

        public WorkflowStep GetFirstStep()
        {
            return _workflowSteps.FirstOrDefault();
        }
    }
}