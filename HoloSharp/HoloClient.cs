using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HoloSharp.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace HoloSharp
{
    public class HoloClient
    {
        private static readonly string url = "https://api.holotools.app/v1/";
        private static HttpClient client = new HttpClient();

        /// <summary>
        /// Retrieve channel data for all VTubers.
        /// </summary>
        /// <param name="limit">Limit the number of VTubers returned. Must be less than or equal to 50.</param>
        /// <param name="offset">Skip a specific number of VTubers.</param>
        /// <returns>An IReadOnlyCollection of VTubers.</returns>
        public IReadOnlyCollection<VTuber> GetAllChannels(int limit = 25, int offset = 0)
        {
            if (limit > 50)
                limit = 50;
            if (limit < 0)
                limit = 0;
            string settings = $"channels?limit={limit}&offset={offset}";
            JObject channels = SendRequest(settings);
            List<VTuber> vtubers = new List<VTuber>();
            foreach (JToken result in channels["channels"]) // skip unnecessary info
            {
                vtubers.Add(result.ToObject<VTuber>());
            }
            return vtubers;
        }

        /// <summary>
        /// Retrieve channel data for the specified VTuber
        /// </summary>
        /// <param name="name">The name of the VTuber.</param>
        /// <remarks>The full channel name is not required, however the database will return nothing if an invalid name is entered.</remarks>
        /// <returns>A <see cref="VTuber"/> matching the given name.</returns>
        public VTuber GetChannelByName(string name)
        {
            string settings = $"channels?name={name}";
            JObject v = SendRequest(settings);
            return v.SelectToken("channels").First.ToObject<VTuber>();
        }

        /// <summary>
        /// Retrieve channel data for the specified VTuber's YouTube channel ID.
        /// </summary>
        /// <param name="youtubeId">The unique YouTube ID of the VTuber</param>
        /// <returns>A <see cref="VTuber"/> matching the given ID.</returns>
        public VTuber GetChannelById(string youtubeId)
        {
            string settings = $"channels/youtube/{youtubeId}";
            return SendRequest(settings).ToObject<VTuber>();
        }

        /// <summary>
        /// Retrieve stream statuses for all VTubers.
        /// </summary>
        /// <param name="maxHours">The maximum amount of hours to search for upcoming streams.</param>
        /// <param name="lookbackHours">The maximum amount of hours to look back for ended streams. Must be less than 12.</param>
        /// <param name="hideChannelDescription">Hide the channel descriptions in the response. Mostly used as a bandwidth saver.</param>
        /// <param name="includeFreeChat">Include upcoming streams marked as "Chat Rooms", "Free Chats", etc.<para />These can usually be avoided by setting a maxHour, however
        /// this makes sure that none appear.</param>
        /// <returns>A <see cref="StreamStatus"/> containing a collection of <see cref="Stream"/> for live, upcoming, and ended streams.</returns>
        public StreamStatus GetStreams(int maxHours = 0, int lookbackHours = 0, bool hideChannelDescription = true, bool includeFreeChat = true)
        {
            char hide = hideChannelDescription ? '1' : '0';
            string settings = $"live?max_upcoming_hours={maxHours}&lookback_hours={lookbackHours}&hide_channel_desc={hide}";
            JObject streams = SendRequest(settings);
            List<Stream> live = new List<Stream>();
            List<Stream> upcoming = new List<Stream>();
            List<Stream> ended = new List<Stream>();
            foreach (JToken t in streams["live"])
                live.Add(t.ToObject<Stream>());
            foreach (JToken t in streams["upcoming"])
                upcoming.Add(t.ToObject<Stream>());
            foreach (JToken t in streams["ended"])
                ended.Add(t.ToObject<Stream>());

            if(!includeFreeChat)
            {
                RegexOptions options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                // Makes sure it doesn't remove stream titles with: chit chat, chatting, midnight chat, etc.
                upcoming.RemoveAll(stream => Regex.Match(stream.Title, @"(?=free|room)(free\s){0,1}(chat(ting){0,1}|talk)(\sroom){0,1}", options).Success);
            }

            return new StreamStatus
            {
                Live = live,
                Upcoming = upcoming,
                Ended = ended
            };
        }

        /// <summary>
        /// Retrieve videos and/or streams from the HoloTools database.
        /// </summary>
        /// <remarks>Timestamped comments are not returned in a bulk search, and will always be empty.</remarks>
        /// <param name="limit">The limit of videos to retrieve. Must be less than or equal to 50.</param>
        /// <param name="offset">How many results to offset by.</param>
        /// <param name="startDate">Only retrieve videos after the given <see cref="DateTime"/></param>
        /// <param name="endDate">Only retrieve videos before the given <see cref="DateTime"/></param>
        /// <param name="type">The specific <see cref="VideoStatus"/> to retrieve.</param>
        /// <param name="isUploaded">When true, only return uploaded videos and not streams.</param>
        /// <param name="isCaptioned">When true, only return videos with closed captions.</param>
        /// <returns>A ReadOnlyCollection of retrieved videos.</returns>
        public IReadOnlyCollection<Video> GetVideos(int limit = 25, int offset = 0, DateTime? startDate = null, DateTime? endDate = null, VideoStatus status = VideoStatus.ANY,
            bool isUploaded = false, bool isCaptioned = false)
        {
            char uploaded = isUploaded ? '1' : '0';
            char captioned = isCaptioned ? '1' : '0';
            if (limit > 50)
                limit = 50;
            if (limit < 0)
                limit = 0;
            StringBuilder build = new StringBuilder($"videos?limit={limit}&offset={offset}");
            if (startDate.HasValue)
                build.Append($"&start_date={startDate.Value:yyyy-MM-dd}");
            if (endDate.HasValue)
                build.Append($"&end_date={endDate.Value:yyyy-MM-dd}");
            if (status != VideoStatus.ANY)
                build.Append($"&status={status.ToString().ToLower()}");
            build.Append($"&is_uploaded={uploaded}&is_captioned={captioned}");
            List<Video> videos = new List<Video>();
            JObject vids = SendRequest(build.ToString());
            foreach (JToken t in vids["videos"])
                videos.Add(t.ToObject<Video>());
            return videos;
                
        }

        /// <summary>
        /// Retrieve video(s) by using a title query.
        /// </summary>
        /// <remarks>Timestamped comments are not returned in a bulk search, and will always be empty.</remarks>
        /// <param name="title">The title of the video to search for.</param>
        /// <remarks>The full video name is not required, however the database will return nothing if a title with no results is used.</remarks>
        /// <returns>A ReadOnlyCollection of <see cref="Video"/> matching the search query.</returns>
        public IReadOnlyCollection<Video> GetVideoByTitle(string title)
        {
            string settings = "videos?title=" + title.Replace(" ", "%20");
            List<Video> videos = new List<Video>();
            JObject vids = SendRequest(settings);
            foreach (JToken t in vids["videos"])
                videos.Add(t.ToObject<Video>());
            return videos;
        }

        /// <summary>
        /// Retrieve a single video using its unique HoloTools database ID.
        /// </summary>
        /// <param name="id">The database ID to use in the search.</param>
        /// <param name="withComments">Whether or not to returned timestamped comments on the video.</param>
        /// <returns>The <see cref="Video"/> matching the given ID.</returns>
        public Video GetVideoByDatabaseId(int id, bool withComments = false)
        {
            string settings = $"videos/{id}?with_comments={(withComments ? '1' : '0')}";
            return SendRequest(settings).ToObject<Video>();
        }

        /// <summary>
        /// Retrieve a single video using its unqiue YouTube video ID.
        /// </summary>
        /// <param name="id">The YouTube video ID to use in the search.</param>
        /// <param name="withComments">Whether or not to returned timestamped comments on the video.</param>
        /// <returns>The <see cref="Video"/> matching the given ID.</returns>
        public Video GetVideoByYoutubeId(string id, bool withComments = false)
        {
            string settings = $"videos/youtube/{id}?with_comments={(withComments ? '1' : '0')}";
            JObject vid = SendRequest(settings);
            Video video = vid.ToObject<Video>();
            List<Comment> comments = new List<Comment>();
            foreach (JToken t in vid["comments"])
                comments.Add(t.ToObject<Comment>());
            video.TimestampedComments = comments;
            return video;
        }

        /// <summary>
        /// Retrieves all videos with timestamped comments containing the given query.
        /// </summary>
        /// <remarks>Queries are automatically tested against English and Japanese comments, therefore both will be retrieved.</remarks>
        /// <param name="query">The text to search for in timestamped comments.</param>
        /// <param name="limit">The limit of video matches to return. Must be less than or equal to 50.</param>
        /// <param name="offset">How many videos to skip in the search.</param>
        /// <param name="id">The unique HoloTools channel id to limit the search to.</param>
        /// <returns>A ReadOnlyCollection of <see cref="Video"/> containing matching timestamped comments.</returns>
        public IReadOnlyCollection<Video> GetComments(string query, int limit = 25, int offset = 0, int id = 0)
        {
            if (limit > 50)
                limit = 50;
            if (limit < 0)
                limit = 0;
            string settings = $"comments/search?q={query.Replace(" ", "%20")}&limit={limit}&offset={offset}";
            if (id > 0)
                settings += $"&channel_id={id}";
            JObject vids = SendRequest(settings);
            List<Video> videos = new List<Video>();
            foreach (JToken t in vids["comments"])
            {
                Video v = t.ToObject<Video>();
                List<Comment> comments = new List<Comment>();
                foreach (JToken c in t["comments"])
                {
                    comments.Add(c.ToObject<Comment>());
                }
                v.TimestampedComments = comments;
                videos.Add(v);
            }
            return videos;

        }

        private JObject SendRequest(string settings)
        {
            string response = "";
            try
            {
                response = client.GetAsync(url + settings).Result.Content.ReadAsStringAsync().Result;
                // TODO: Error Handling :D
            }
            catch (WebException e)
            {
                throw new WebException(e.Message);
            }

            JObject result = JObject.Parse(response);
            // In the case of an error, the HoloTools API returns JSON with a single object named message
            // Check for this element before returning the result
            if (result["message"] != null)
                throw new ArgumentException((string)result["message"]);
            return result;
        }
    }
}
