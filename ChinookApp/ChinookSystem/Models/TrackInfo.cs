using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Models
{
    public class TrackInfo
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public string AlbumTitle { get; set; }
        public string ArtistName { get; set; }
        public int Milliseconds { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
