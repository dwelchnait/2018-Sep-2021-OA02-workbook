using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Models
{
    public class PLTrackInfo
    {
        public int TrackId { get; set; }
        public int TrackNumber { get; set; }
        public string Song { get; set; }
        public int Milliseconds { get; set; }

        public TimeSpan RunningTime
        {
            get
            {
                return TimeSpan.FromMilliseconds(Milliseconds);
            }
        }
    }
}
