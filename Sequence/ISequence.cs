using System.Threading.Tasks;

namespace Sequence
{
    public interface ISequence
    {
        Task ExecuteAsync();
    }
}