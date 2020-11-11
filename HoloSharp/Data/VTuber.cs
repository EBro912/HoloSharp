using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HoloSharp.Data
{
    /// <summary>
    /// Represents a VTuber's Channel.
    /// </summary>
    public sealed class VTuber
    {
        /// <summary>
        /// The unique ID of the VTuber in the HoloTools database.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; internal set; }

        /// <summary>
        /// The unique ID of the VTuber's YouTube channel.
        /// </summary>
        [JsonProperty("yt_channel_id")]
        public string ChannelId { get; internal set; }

        /// <summary>
        /// The VTuber's name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// The VTuber's YouTube channel description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        /// The URL of the VTuber's profile picture.
        /// </summary>
        [JsonProperty("photo")]
        public string Photo { get; internal set; }

        /// <summary>
        /// The date in which the VTuber's YouTube channel was created at.
        /// </summary>
        [JsonProperty("published_at")]
        public DateTime? CreatedAt { get; internal set; }

        /// <summary>
        /// The VTuber's Twitter username.
        /// </summary>
        [JsonProperty("twitter_link")]
        public string TwitterHandle { get; internal set; }

        /// <summary>
        /// The VTuber's estimated total view count.
        /// </summary>
        [JsonProperty("view_count")]
        public int ViewCount { get; internal set; }

        /// <summary>
        /// The VTuber's estimated subscriber count.
        /// </summary>
        [JsonProperty("subscriber_count")]
        public int SubscriberCount { get; internal set; }

        /// <summary>
        /// The total number of videos and streams this VTuber has posted.
        /// </summary>
        [JsonProperty("video_count")]
        public int VideoCount { get; internal set; }

    }
}
