namespace Sequence
{
    using System.Threading.Tasks;

    internal class Sequence : ISequence
    {
        private readonly ISequenceReader _stepReader;
        private readonly ISequenceContext _context;

        internal Sequence(ISequenceReader stepReader, ISequenceContext context)
        {
            _stepReader = stepReader;
            _context = context;
        }

        public virtual async Task ExecuteAsync()
        {
            var firstStep = _stepReader.GetFirstStep();

            if(firstStep != null)
                await firstStep.RunAsync(_context);
        }
    }
}
