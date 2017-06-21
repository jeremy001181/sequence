using System.Threading.Tasks;

namespace Sequence
{
    public abstract class Step
    {
        public abstract Task RunAsync(ISequenceContext context);

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
