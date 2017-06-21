using System;
using System.Threading.Tasks;

namespace Sequence
{
    internal class ActionStep : Step
    {
        private readonly Action<ISequenceContext, Func<ISequenceContext, Task>> _action;

        internal ActionStep(Action<ISequenceContext, Func<ISequenceContext, Task>> action)
        {
            _action = action;
        }

        public override async Task RunAsync(ISequenceContext context)
        {
            _action(context, async sequenceContext =>
            {
                await Next(sequenceContext);
            });

            await Task.FromResult(0);
        }
    }
}