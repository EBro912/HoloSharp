using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HoloSharp.Data;
using Newtonsoft.Json;

namespace HoloSharp
{
    public class HoloSharp
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
            if (limit <= 0)
                limit = 1;
            string settings = $"channels?limit={limit}&offset={offset}";
            return JsonConvert.DeserializeObject<IReadOnlyCollection<VTuber>>(SendRequest(settings)); // send the request
        }

        /// <summary>
        /// Retrieve channel data for the specified VTuber
        /// </summary>
        /// <param name="name">The name of the VTuber.</param>
        /// <returns>A <see cref="VTuber"/> matching the given name.</returns>
        public VTuber GetChannelByName(string name)
        {
            string settings = $"channels?name={name}";
            return JsonConvert.DeserializeObject<VTuber>(SendRequest(settings));
        }

        /// <summary>
        /// Retrieve channel data for the specified VTuber's YouTube channel ID.
        /// </summary>
        /// <param name="youtubeId">The unique YouTube ID of the VTuber</param>
        /// <returns>A <see cref="VTuber"/> matching the given ID.</returns>
        public VTuber GetChannelById(string youtubeId)
        {
            string settings = $"channels/youtube/{youtubeId}";
            return JsonConvert.DeserializeObject<VTuber>(SendRequest(settings));
        }

        /// <summary>
        /// Retrieve stream statuses for all VTubers.
        /// </summary>
        /// <param name="maxHours">The maximum amount of hours to search for upcoming streams.</param>
        /// <param name="lookbackHours">The maximum amount of hours to look back for ended streams.</param>
        /// <param name="hideChannelDescription">Hide the channel descriptions in the response. Mostly used as a bandwidth saver.</param>
        /// <returns>A <see cref="StreamStatus"/> containing a collection of <see cref="Stream"/> for live, upcoming, and ended streams.</returns>
        public StreamStatus GetStreamStatuses(int maxHours = 0, int lookbackHours = 0, bool hideChannelDescription = true)
        {
            int hide = hideChannelDescription ? 1 : 0;
            string settings = $"live?max_upcoming_hours={maxHours}&lookback_hours={lookbackHours}&hide_channel_desc={hide}";
            return JsonConvert.DeserializeObject<StreamStatus>(SendRequest(settings));
        }


        private string SendRequest(string settings)
        {
            return client.GetAsync(url + settings).Result.Content.ReadAsStringAsync().Result;
            // TODO: Error Handling
        }
    }
}
