using System;

namespace Sequence
{
    public class SequenceFactory : ISequenceFactory
    {
        public ISequence CreateSequence(Action<IStepCollectionBuilder> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var stepCollection = new StepManager();

            action(stepCollection);

            return new Sequence(stepCollection, new SequenceContext());
        }
    }

    public interface ISequenceReader
    {
        Step GetFirstStep();
    }
}