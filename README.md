# Sequence

A simple sequence framework that executes steps in sequence. This is implemented based on sequence pattern [here](http://www.workflowpatterns.com/patterns/control/basic/wcp1.php).

    var sequence = new SequenceFactory().CreateSequence(builder =>
    {
        builder
          .AddStep<ComplexStepToRun>()
          .AddStep(async (context, next) =>
          {
              await next(context);
          });
    });

    await sequence.ExecuteAsync();
