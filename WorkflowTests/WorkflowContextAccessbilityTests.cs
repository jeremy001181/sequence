using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Workflow;

namespace WorkflowTests
{
    [TestFixture]
    class WorkflowContextAccessbilityTests
    {
        private readonly IWorkflowFactory _factory = new WorkflowFactory();

        [Test]
        public async Task Should_be_able_to_read_object_that_set_in_previous_step_from_context()
        {
            var workflow = _factory.CreateWorkflow(builder =>
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

            await workflow.ExecuteAsync();
        }
    }

    public class TestStepToAddObject : WorkflowStep
    {
        public override async Task RunAsync(IWorkflowContext context)
        {
            context.Data.Add("testdata1", "testdata1");

            await Next(context);
        }
    }

    internal class TestStepB : WorkflowStep
    {
        public override async Task RunAsync(IWorkflowContext context)
        {
            Assert.AreEqual(context.Data["testdata1"], "testdata1");

            context.Data.Add("testdata2", "testdata2");

            await Next(context);
        }
    }
}
