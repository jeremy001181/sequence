using System;

namespace Sequence
{
    public class SequenceFactory : ISequenceFactory
    {
        public ISequence CreateSequence(Action<IStepCollectionBuilder> configureSteps)
        {
            if (configureSteps == null)
            {
                throw new ArgumentNullException("configureSteps");
            }

            var stepCollection = new StepManager();

            configureSteps(stepCollection);

            return new Sequence(stepCollection, new SequenceContext());
        }
    }
}