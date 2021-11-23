using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional 
using ChinookSystem.Models;
using ChinookSystem.DAL;
using ChinookSystem.Entities;
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

        #region Playlist Track Maintenance
        public void PlaylistTrack_AddTrack(string playlistname,
                                           string username,
                                           int trackid)
        {
            Playlist playlistExist = null;
            PlaylistTrack playlisttrackExist = null;
            int tracknumber = 0;

            //Business Logic is the rules of your business
            //   rule: a track can only exist once on a playlist
            //   rule: each track on a playlist is assigned a continuous
            //          track number
            //
            //The BLL method should also ensure that data exists for
            //   the procesing of the transaction

            //check my input to this method to ensure I have data
            if (string.IsNullOrWhiteSpace(playlistname))
            {
                throw new Exception("Playlist name is missing. Unable to add track.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("User name is missing. Unable to add track.");
            }

            //does the playlist exist?
            playlistExist = _context.Playlists
                                    .Where(x => x.Name.Equals(playlistname)
                                             && x.UserName.Equals(username))
                                    .FirstOrDefault();
            if (playlistExist == null)
            {
                //no existing playlist, create one
                playlistExist = new Playlist()
                {
                    Name = playlistname,
                    UserName = username
                };
                _context.Playlists.Add(playlistExist);
                tracknumber = 1;                
            }
            else
            {
                playlisttrackExist = _context.PlaylistTracks
                           .Where(x => x.Playlist.Name.Equals(playlistname)
                                    && x.Playlist.UserName.Equals(username)
                                    && x.TrackId == trackid)
                           .FirstOrDefault();
                if (playlisttrackExist != null)
                {
                    throw new Exception("TracK already on playlist");
                }
                else
                {
                    //find the next tracknumber
                    //do this automatically for the system
                    tracknumber = _context.PlaylistTracks
                           .Where(x => x.Playlist.Name.Equals(playlistname)
                                    && x.Playlist.UserName.Equals(username))
                           .Select(x => x.TrackNumber)
                           .Max();
                    tracknumber++;
                }
            }
        }
        #endregion
    }
}
