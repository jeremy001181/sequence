using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sequence.AcceptanceTests
{
    [TestFixture]
    class ContextAccessbilityTests
    {
        private readonly ISequenceFactory _factory = new SequenceFactory();

        [Test]
        public async Task Should_be_able_to_read_object_that_set_in_previous_step_from_context()
        {
            var sequence = _factory.CreateSequence(builder =>
            {
                builder
                    .AddStep<TestSteps.AddArgumentsToContextStep>("1", "step 1")
                    .AddStep(async (context, next) =>
                    {
                        Assert.AreEqual(context.Data["1"], "step 1");
                        await next(context);
                    });
                var obj = new object();
                builder.AddStep<TestSteps.AddArgumentsToContextStep>("2", obj);
                builder.AddStep(async (context, next) =>
                {
                    Assert.AreEqual(context.Data["1"], "step 1");
                    Assert.AreSame(context.Data["2"], obj);

                    await next(context);
                });
            });

            await sequence.ExecuteAsync();
        }

        [Test]
        public async Task Should_be_able_to_read_object_once_sequence_completed_execution()
        {
            var sequence = _factory.CreateSequence(builder =>
            {
                builder.AddStep<TestSteps.AddArgumentsToContextStep>("1", "step 1");
            });

            await sequence.ExecuteAsync();

            Assert.AreEqual(sequence.Context.Data["1"], "step 1");
        }

        [Test]
        public async Task Should_be_able_to_read_data_from_context_after_exception_occurred()
        {
            var sequence = _factory.CreateSequence(builder =>
            {
                builder.AddStep<TestSteps.AddArgumentsToContextStep>("1", "step 1")
                       .AddStep((context, next) =>
                    {
                        throw new NotImplementedException();
                    });
            });

            try
            {
                await sequence.ExecuteAsync();
            }
            catch
            {
                Assert.AreEqual(sequence.Context.Data["1"], "step 1");
            }
           
        }
    }
}
