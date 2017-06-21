using System;

namespace Sequence
{
    public interface ISequenceFactory
    {
        ISequence CreateSequence(Action<IStepCollectionBuilder> action);
    }
}