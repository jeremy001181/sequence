using System.Collections.Generic;

namespace Sequence
{
    /// <summary>
    /// This is channel how each individual sequence step communicates each other and 
    /// </summary>
    public interface ISequenceContext
    {
        /// <summary>
        /// A dictionary for storing and passing data between each steps during sequence execution
        /// </summary>
        IDictionary<string, object> Data { get; set; }
    }
}