using System;

namespace Sequence
{
    /// <summary>
    /// Sequence factory for create sequence
    /// </summary>
    public class SequenceFactory : ISequenceFactory
    {
        /// <inheritdoc />
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