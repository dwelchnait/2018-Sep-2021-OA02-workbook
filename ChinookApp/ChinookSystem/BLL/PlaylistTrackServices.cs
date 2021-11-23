using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional 
using ChinookSystem.Models;
using ChinookSystem.DAL;
#endregion


namespace ChinookSystem.BLL
{
    public class PlaylistTrackServices
    {
        #region Constructor and DI variable setup
        private readonly ChinookContext _context;

        internal PlaylistTrackServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public List<PLTrackInfo> Tracks_GetPlaylistforUser(string playlistname, string username)
        {
            IEnumerable<PLTrackInfo> info = _context.PlaylistTracks
                            .Where(x => x.Playlist.Name.Equals(playlistname) 
                                     && x.Playlist.UserName.Equals(username))
                            .Select(x => new PLTrackInfo
                            {
                                TrackId = x.TrackId,
                                TrackNumber = x.TrackNumber,
                                Song = x.Track.Name,
                                Milliseconds = x.Track.Milliseconds

                            })
                            .OrderBy(x => x.TrackNumber);
            return info.ToList();
        }
        #endregion

    }
}
