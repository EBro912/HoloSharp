using Newtonsoft.Json;
using System.Collections.Generic;

namespace HoloSharp.Data
{
    public sealed class Video : Stream 
    {
        /// <summary>
        /// Whether or not the video is uploaded and is not a stream.
        /// </summary>
        [JsonProperty("is_uploaded")]
        public bool? isUploaded { get; internal set; }
        
        /// <summary>
        /// The duration of the video, in seconds.
        /// </summary>
        [JsonProperty("duration_secs")]
        public int? Duration { get; internal set; }

        /// <summary>
        /// Whether or not the video has closed captions.
        /// </summary>
        [JsonProperty("is_captioned")]
        public bool? isCaptioned { get; internal set; }

        /// <summary>
        /// Any comments on the video which have a timestamp.
        /// </summary>
        [JsonProperty("comments")]
        public IReadOnlyCollection<Comment> TimestampedComments { get; internal set; } = new List<Comment>();
    }
}
