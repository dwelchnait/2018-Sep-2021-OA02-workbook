using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Models
{
    /// <summary>
    /// check out the following for simply diagram showing cqrs
    /// 
    /// sookocheff.com/post/architecture/what-is-cqrs
    /// </summary>
    ////record : Query of CQRS
    public class PLTrackInfo
    {
        //retreived database data
        public int TrackId { get; set; }
        public int TrackNumber { get; set; }
        public string Name { get; set; }
        public int Milliseconds { get; set; }

        //display data readonly
        public TimeSpan RunningTime
        {
            get
            {
                return TimeSpan.FromMilliseconds(Milliseconds);
            }
        }


    }

    ////record : Command of CQRS
    public record CTrackInfo(int TrackId, string SelectedTrack, string InputData)

    {
        public CTrackInfo() : this(0, null, null) { }

        public bool Checked()
        {
            return SelectedTrack is null ? false : true;
        }
    }
}
