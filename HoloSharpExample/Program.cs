using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using HoloSharp;
using HoloSharp.Data;

namespace HoloSharpExample
{
    public class Program
    {
        public static HoloSharp.HoloSharp holoSharp = new HoloSharp.HoloSharp(); 

        // Toy around with our results
        public static void Main(string[] args)
        {
            Console.WriteLine("Current Live Streams: " + GetLiveStreams().Count);
            Console.WriteLine("Returned Twitter Handle: " + TwitterHandle("Pekora"));
            foreach (Video v in GetVideos(new DateTime(2020, 6, 20))) // Get all videos posted after June 20th, 2020
            {
                Console.WriteLine(v.Title);
            }
        }

        // Example A: Retrieve all current live streams
        public static IReadOnlyCollection<Stream> GetLiveStreams()
        {
            return holoSharp.GetStreams().Live;
        }

        // Example B: Retrieve a VTuber's Twitter Handle by Name
        public static string TwitterHandle(string name)
        {
            VTuber vtuber = holoSharp.GetChannelByName(name);
            return vtuber.TwitterHandle;
        }

        // Example C: Retrieve all videos posted after a certain time
        public static IReadOnlyCollection<Video> GetVideos(DateTime start)
        {
            return holoSharp.GetVideos(startDate: start, isUploaded: true);
        }
    }
}
