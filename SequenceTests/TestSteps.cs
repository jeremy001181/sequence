using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequence.AcceptanceTests
{
    class TestSteps
    {
        internal class AddArgumentsToContextStep : Step
        {
            private readonly string _key;
            private readonly object _value;

            public AddArgumentsToContextStep(string key, object value)
            {
                _key = key;
                _value = value;
            }
            public override async Task RunAsync(ISequenceContext context)
            {
                context.Data.Add(_key, _value);

                await Next(context);
            }
        }

        internal class ThrowUnhandleExceptionStep : Step
        {
            public override async Task RunAsync(ISequenceContext context)
            {
                await Task.FromResult(0);

                throw new InvalidOperationException();
            }
        }

        internal class EmptyStep : Step
        {
            public override async Task RunAsync(ISequenceContext context)
            {
                await Next(context);
            }
        }
    }
}
