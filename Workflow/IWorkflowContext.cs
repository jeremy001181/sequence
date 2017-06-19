using System.Collections.Generic;

namespace Workflow
{
    public interface IWorkflowContext
    {
        IDictionary<string, object> Data { get; set; }
    }
}