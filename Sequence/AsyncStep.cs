using System;
using System.Threading.Tasks;

namespace Sequence
{
    internal class AsyncStep : Step
    {
        private readonly Func<ISequenceContext, Func<ISequenceContext, Task>, Task> _asyncFunc;

        internal AsyncStep(Func<ISequenceContext, Func<ISequenceContext, Task>, Task> asyncFunc)
        {
            _asyncFunc = asyncFunc;
        }

        public override async Task RunAsync(ISequenceContext context)
        {
            await _asyncFunc(context, async sequenceContext =>
            {
                await Next(sequenceContext);
            });

            await Task.FromResult(0);
        }
    }
}