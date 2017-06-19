using System.Collections.Generic;
using System.Linq;

namespace Workflow.Extensions
{
    internal static class ListExtensions
    {
        internal static void AppendWorkflowStep(this IList<WorkflowStep> workflowStepList, WorkflowStep newStep)
        {
            var lastStep = workflowStepList.LastOrDefault();

            if (lastStep != null)
            {
                lastStep.NextStep = newStep;
            }

            workflowStepList.Add(newStep);
        }
    }
}
