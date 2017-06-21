﻿using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Sequence;

namespace SequenceTests
{
    [TestFixture]
    public class SequenceOfStepsExecutionTests
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
            var workflow = _factory.CreateSequence(builder =>
            {
            });

            await workflow.ExecuteAsync();

            Assert.Pass();
        }

        [Test]
        public async Task Should_throw_null_argument_exception_when_no_action_provided_for_building_steps()
        {
            Assert.Throws<ArgumentNullException>(() => _factory.CreateSequence(null));
        }
    }

    public class SimpleStepNotRun : Step
    {
        public override async Task RunAsync(ISequenceContext context)
        {
            await Next(context);
        }
    }

    public class SimpleStepNoParameters : Step
    {
        public override async Task RunAsync(ISequenceContext context)
        {
            await Next(context);
        }
    }

    public class SimpleStepWithParameters : Step
    {
        private readonly string _something;

        public SimpleStepWithParameters(string something)
        {
            _something = something;
        }

        public override async Task RunAsync(ISequenceContext context)
        {
            await Next(context);
        }
    }
}
