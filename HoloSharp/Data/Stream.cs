using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoloSharp.Data
{
    /// <summary>
    /// Represents a Stream on YouTube. It can be live, upcoming, or ended.
    /// </summary>
    public class Stream
    {
        /// <summary>
        /// The unique ID of the Stream in the HoloTools database.
        /// </summary>
        [JsonProperty("id")]
        public int id { get; internal set; }

        /// <summary>
        /// The unique ID of the VTuber's YouTube channel.
        /// </summary>
        [JsonProperty("yt_video_key")]
        public string VideoKey { get; internal set; }

        /// <summary>
        /// The title of the stream.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; internal set; }

        /// <summary>
        /// The URL of the stream's thumbnail.
        /// </summary>
        [JsonProperty("thumbnail")]
        public string Thumbnail { get; internal set; }

        /// <summary>
        /// The <see cref="DateTime"/> when the stream is scheduled to start.
        /// </summary>
        [JsonProperty("live_schedule")]
        public DateTime? scheduledTime { get; internal set; }

        /// <summary>
        /// The <see cref="DateTime"/> when the stream started.
        /// </summary>
        [JsonProperty("live_start")]
        public DateTime? startTime { get; internal set; }

        /// <summary>
        /// The <see cref="DateTime"/> when the stream ended.
        /// </summary>
        [JsonProperty("live_end")]
        public DateTime? endTime { get; internal set; }

        /// <summary>
        /// The estimated number of viewers the stream currently has.
        /// </summary>
        [JsonProperty("live_viewers")]
        public int? Viewers { get; internal set; }

        /// <summary>
        /// The <see cref="VTuber"/> who is running the stream.
        /// </summary>
        [JsonProperty("channel")]
        public VTuber Channel { get; internal set; }
    }
}
