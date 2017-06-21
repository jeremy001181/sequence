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
                builder.AddStep<AddArgumentsToContextStep>("1", "step 1")
                    .AddStep(async (context, next) =>
                    {
                        Assert.AreEqual(context.Data["1"], "step 1");
                        await next(context);
                    });
                var obj = new object();
                builder.AddStep<AddArgumentsToContextStep>("2", obj);
                builder.AddStep(async (context, next) =>
                {
                    Assert.AreEqual(context.Data["1"], "step 1");
                    Assert.AreSame(context.Data["2"], obj);

                    await next(context);
                });
            });

            await sequence.ExecuteAsync();
        }
    }

    public class AddArgumentsToContextStep : Step
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
}
