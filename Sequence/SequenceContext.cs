using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Sequence
{
    public class SequenceContext : ISequenceContext
    {
        public SequenceContext()
        {
            //todo:concurrentdict?
            Data = new ConcurrentDictionary<string, object>();
        }

        public IDictionary<string, object> Data { get; set; }
    }
}