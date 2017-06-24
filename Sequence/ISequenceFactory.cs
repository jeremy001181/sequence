using System;

namespace Sequence
{
    public interface ISequenceFactory
    {
        /// <summary>
        /// Create a new sequence of steps
        /// </summary>
        /// <param name="configureSteps">Specify how your steps run</param>
        /// <returns>A new Sequence object</returns>
        ISequence CreateSequence(Action<IStepCollectionBuilder> configureSteps);
    }
}