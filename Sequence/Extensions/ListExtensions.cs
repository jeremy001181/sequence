using System.Collections.Generic;
using System.Linq;

namespace Sequence.Extensions
{
    internal static class ListExtensions
    {
        internal static void AppendStep(this IList<Step> stepList, Step newStep)
        {
            var lastStep = stepList.LastOrDefault();

            if (lastStep != null)
            {
                lastStep.NextStep = newStep;
            }

            stepList.Add(newStep);
        }
    }
}
