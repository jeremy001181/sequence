using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Workflow
{
    public class WorkflowContext : IWorkflowContext
    {
        public WorkflowContext()
        {
            //todo:concurrentdict?
            Data = new ConcurrentDictionary<string, object>();
        }

        public IDictionary<string, object> Data { get; set; }
    }
}