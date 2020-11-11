using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoloSharp;
using HoloSharp.Data;

namespace HoloSharpExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            HoloSharp.HoloSharp hs = new HoloSharp.HoloSharp();
            IReadOnlyCollection<Video> vid = hs.GetComments("among us");
            Console.WriteLine(vid.Count);
            foreach (Video v in vid)
            {
                foreach (Comment c in v.TimestampedComments)
                {
                    Console.WriteLine(c.Message);
                }
            }
            watch.Stop();
            Console.WriteLine("Took " + watch.ElapsedMilliseconds + "ms");
            // HoloSharp.Data.StreamStatus status = hs.GetStreamStatuses(72, 12);
            // Console.WriteLine($"{status.Live.Count} | {status.Upcoming.Count} | {status.Ended.Count}");
        }
    }
}
