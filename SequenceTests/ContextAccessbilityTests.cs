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
                builder.AddStep<TestStepToAddObject>();
                builder.AddStep<TestStepB>();
                builder.AddStep(async (context, next) =>
                {
                    Assert.AreEqual(context.Data["testdata1"], "testdata1");
                    Assert.AreEqual(context.Data["testdata2"], "testdata2");

                    await next(context);
                });
            });

            await sequence.ExecuteAsync();
        }
    }

    public class TestStepToAddObject : Step
    {
        public override async Task RunAsync(ISequenceContext context)
        {
            context.Data.Add("testdata1", "testdata1");

            await Next(context);
        }
    }

    internal class TestStepB : Step
    {
        public override async Task RunAsync(ISequenceContext context)
        {
            Assert.AreEqual(context.Data["testdata1"], "testdata1");

            context.Data.Add("testdata2", "testdata2");

            await Next(context);
        }
    }
}
