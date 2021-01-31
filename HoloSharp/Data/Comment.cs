using Newtonsoft.Json;

namespace HoloSharp.Data
{
    /// <summary>
    /// Represents a timestamped comment on a YouTube video.
    /// </summary>
    public sealed class Comment
    {
        /// <summary>
        /// The comment's unique key.
        /// </summary>
        [JsonProperty("comment_key")]
        public string Key { get; internal set; }

        /// <summary>
        /// The text of the comment. The start of the comment will be the timestamp.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; internal set; }
    }
}
