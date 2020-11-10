﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoloSharp.Data
{
    /// <summary>
    /// The Status of all streams.
    /// </summary>
    public sealed class StreamStatus
    {
        /// <summary>
        /// A collection of all currently live streams.
        /// </summary>
        public IReadOnlyCollection<VTuber> Live { get; internal set; }
        /// <summary>
        /// A collection of all upcoming streams.
        /// </summary>
        public IReadOnlyCollection<VTuber> Upcoming { get; internal set; }
        /// <summary>
        /// A collection of streams which have ended.
        /// </summary>
        public IReadOnlyCollection<VTuber> Ended { get; internal set; }
    }
}
