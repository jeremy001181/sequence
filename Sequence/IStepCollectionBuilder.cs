using System;
using System.Threading.Tasks;

namespace Sequence
{
    /// <summary>
    /// Step builder
    /// </summary>
    public interface IStepCollectionBuilder
    {
        /// <summary>
        /// Add a strong type sequence step
        /// </summary>
        /// <param name="args"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IStepCollectionBuilder AddStep<T>(params object [] args) where T : Step;
        /// <summary>
        /// Add a async delegate as a step
        /// </summary>
        /// <param name="asyncStep"></param>
        /// <returns></returns>
        IStepCollectionBuilder AddStep(Func<ISequenceContext, Func<ISequenceContext, Task>, Task> asyncStep);
    }
}