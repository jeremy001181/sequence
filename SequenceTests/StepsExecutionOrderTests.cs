using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sequence.AcceptanceTests
{
    [TestFixture]
    public class StepsExecutionOrderTests
    {
        private readonly ISequenceFactory _factory = new SequenceFactory();

        [Test]
        public async Task Should_execute_steps_in_the_same_order_as_they_were_added()
        {
            var count = 1;
            var sequence = _factory.CreateSequence(builder =>
            {
                builder
                    .AddStep(async (context, next) =>
                    {
                        Assert.AreEqual(1, count++);

                        await next(context);
                    })
                    .AddStep(async (context, next) =>
                    {
                        Assert.AreEqual(2, count++);

                        await next(context);
                    })
                    .AddStep(async (context, next) =>
                    {
                        Assert.AreEqual(3, count++);

                        await next(context);
                    });
            });

            await sequence.ExecuteAsync();
        }

        [Test]
        public async Task Should_pass_when_there_are_no_steps()
        {
            var sequence = _factory.CreateSequence(builder =>
            {
            });

            await sequence.ExecuteAsync();

            Assert.Pass();
        }

        [Test]
        public async Task Should_throw_null_argument_exception_when_no_action_provided_for_building_steps()
        {
            Assert.Throws<ArgumentNullException>(() => _factory.CreateSequence(null));
        }

        [Test]
        public async Task Should_not_execute_any_following_steps_when_previous_step_doesnot_invoke_next()
        {
            var count = 0;
            var sequence = _factory.CreateSequence(builder =>
            {
                builder
                    .AddStep(async (context, next) =>
                    {
                        count++;
                        await next(context);
                    })
                    .AddStep(async (context, next) =>
                    {
                        // A step not does anything
                        await Task.FromResult(0);
                    })
                    .AddStep(async (context, next) =>
                    {
                        count++;
                        await next(context);
                    });
            });

            await sequence.ExecuteAsync();

            Assert.AreEqual(1, count);
        }
    }
}
