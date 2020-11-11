using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoloSharp.Data
{
    /// <summary>
    /// The status of a returned video and/or stream.
    /// </summary>
    public enum VideoStatus
    {
        ANY,
        /// <summary>
        /// A video that has been recently posted.
        /// </summary>
        NEW,

        /// <summary>
        /// A stream that is live.
        /// </summary>
        LIVE,

        /// <summary>
        /// A stream that is upcoming.
        /// </summary>
        UPCOMING,

        /// <summary>
        /// A stream that has ended.
        /// </summary>
        PAST,

        /// <summary>
        /// A stream and/or video that is missing.
        /// </summary>
        MISSING
    }
}
