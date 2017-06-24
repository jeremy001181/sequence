using System.Collections.Generic;

namespace Sequence
{
    internal class SequenceContext : ISequenceContext
    {
        internal SequenceContext()
        {
            Data = new Dictionary<string, object>();
        }

        /// <inheritdoc />
        public IDictionary<string, object> Data { get; set; }
    }
}