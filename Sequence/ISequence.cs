using System.Threading.Tasks;

namespace Sequence
{
    /// <summary>
    /// A sequence containing steps to run
    /// </summary>
    public interface ISequence
    {
        /// <summary>
        /// Execute
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();
        /// <summary>
        /// Sequence context
        /// </summary>
        ISequenceContext Context { get; }
    }
}