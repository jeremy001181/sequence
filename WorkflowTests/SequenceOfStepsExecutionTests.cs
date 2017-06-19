using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Workflow;

namespace WorkflowTests
{
    [TestFixture]
    public class SequenceOfStepsExecutionTests
    {
        private readonly IWorkflowFactory _factory = new WorkflowFactory();

        [Test]
        public async Task Should_execute_steps_in_the_same_order_as_they_were_added()
        {
            var count = 1;
            var workflow = _factory.CreateWorkflow(builder =>
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

            await workflow.ExecuteAsync();
        }

        [Test]
        public async Task Should_pass_when_there_are_no_steps()
        {
            var workflow = _factory.CreateWorkflow(builder =>
            {
            });

            await workflow.ExecuteAsync();

            Assert.Pass();
        }

        [Test]
        public async Task Should_throw_null_argument_exception_when_no_action_provided_for_building_steps()
        {
            Assert.Throws<ArgumentNullException>(() => _factory.CreateWorkflow(null));
        }
    }

    public class SimpleWorkflowStepNotRun : WorkflowStep
    {
        public override async Task RunAsync(IWorkflowContext context)
        {
            await Next(context);
        }
    }

    public class SimpleWorkflowStepNoParameters : WorkflowStep
    {
        public override async Task RunAsync(IWorkflowContext context)
        {
            await Next(context);
        }
    }

    public class SimpleWorkflowStepWithParameters : WorkflowStep
    {
        private readonly string _something;

        public SimpleWorkflowStepWithParameters(string something)
        {
            _something = something;
        }

        public override async Task RunAsync(IWorkflowContext context)
        {
            await Next(context);
        }
    }
}
