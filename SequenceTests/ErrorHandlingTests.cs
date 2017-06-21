using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sequence.AcceptanceTests
{
    [TestFixture]
    class ErrorHandlingTests
    {
        private readonly ISequenceFactory _factory = new SequenceFactory();

        [Test]
        public async Task Should_not_swallow_unhandle_exception_in_async_execution_step()
        {
            var sequence = _factory.CreateSequence(builder =>
            {
                builder.AddStep(async (context, next) =>
                {
                    await Task.FromResult(0);

                    throw new InvalidOperationException();
                });
            });

            Assert.ThrowsAsync<InvalidOperationException>(async () => await sequence.ExecuteAsync());
        }

        [Test]
        public async Task Should_not_swallow_unhandle_exception_in_execution_of_strong_type_step()
        {
            var sequence = _factory.CreateSequence(builder =>
            {
                builder.AddStep<ThrowUnhandleExceptionStep>();
            });

            Assert.ThrowsAsync<InvalidOperationException>(async () => await sequence.ExecuteAsync());
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
}
