using System.Threading.Tasks;

namespace Sequence
{
    /// <summary>
    /// An abstract base class for a standard sequence step pattern.
    /// </summary>
    public abstract class Step
    {
        /// <summary>
        /// Run an individual step.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract Task RunAsync(ISequenceContext context);

        /// <summary>
        /// The optional next step to run
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected async Task Next(ISequenceContext context)
        {
            if (NextStep != null)
            {
                await NextStep.RunAsync(context);
            }
        }

        internal Step NextStep { get; set; }
    }
}
