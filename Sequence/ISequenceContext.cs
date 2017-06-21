using System.Collections.Generic;

namespace Sequence
{
    public interface ISequenceContext
    {
        IDictionary<string, object> Data { get; set; }
    }
}