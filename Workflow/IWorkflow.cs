using System.Threading.Tasks;

namespace Workflow
{
    public interface IWorkflow
    {
        Task ExecuteAsync();
    }
}