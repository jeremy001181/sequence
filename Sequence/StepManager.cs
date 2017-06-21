using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Sequence.Extensions;

namespace Sequence
{
    internal class StepManager : IStepCollectionBuilder, ISequenceReader
    {
        private readonly IList<Step> _steps;

        internal StepManager()
        {
            _steps = new List<Step>();
        }

        public IStepCollectionBuilder AddStep<T>(params object[] args) where T : Step, new()
        {
            var newStep = args == null || args.Length == 0
                ? Activator.CreateInstance<T>() 
                : Activator.CreateInstance(typeof(T), BindingFlags.CreateInstance, args);
            
            _steps.AppendStep(newStep as Step);

            return this;
        }
        
        public IStepCollectionBuilder AddStep(Action<ISequenceContext, Func<ISequenceContext, Task>> action)
        {
            var newStep = new ActionStep(action);
            
            _steps.AppendStep(newStep);

            return this;
        }

        public Step GetFirstStep()
        {
            return _steps.FirstOrDefault();
        }
    }
}