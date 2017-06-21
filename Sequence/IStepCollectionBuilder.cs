using System;
using System.Threading.Tasks;

namespace Sequence
{
    public interface IStepCollectionBuilder
    {
        IStepCollectionBuilder AddStep<T>(params object [] args) where T : Step, new();
        IStepCollectionBuilder AddStep(Func<ISequenceContext, Func<ISequenceContext, Task>, Task> asyncStep);
    }
}