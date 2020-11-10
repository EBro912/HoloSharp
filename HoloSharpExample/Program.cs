using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoloSharp;

namespace HoloSharpExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HoloSharp.HoloSharp hs = new HoloSharp.HoloSharp();
            HoloSharp.Data.VTuber v = hs.GetChannelById("UCoSrY_IQQVpmIRZ9Xf-y93g");
            Console.WriteLine(v.CreatedAt);
        }
    }
}
