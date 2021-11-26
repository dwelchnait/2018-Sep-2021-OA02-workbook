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
            //
            //If the business are passed and data is validation
            //  a)stage your transaction work
            //  b)execute a SINGLE .SaveChanges()

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
                //staged ONLY (in memory)
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
                    throw new Exception("Track already on playlist");
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
            //add the track to the playlist
            //create an instance for the track
            playlisttrackExist = new PlaylistTrack();

            //load the properties (fields) of the instance
            playlisttrackExist.TrackId = trackid;
            playlisttrackExist.TrackNumber = tracknumber;

            //??? what about the second part of the primary key: playlist id
            //if the playlist exists then we know th id: playlistExist
            //BUT if the playlist is NEW, we DO NOT know the id

            //in the situation of a NEW playlist, even though we have
            //  created the playlist instance (see above) it is ONLY
            //  staged.
            //this means that the actual sql record has NOT yet been 
            //  created.
            //this means that the IDENTITY value for the new playlist DOES
            //  NOT yet exist. The value on the playlist instance (playlistExist)
            //  is zero (0).
            //Therefore we have a serious problem

            //Solution:
            //  Is built into the EntityFramework and is based on using the
            //  navigational property in Playlist pointing to its "child"

            //staging a typical Add in the pass was to reference the entity
            //   and use the .Add(xxx)
            //_context.PlaylistTracks.Add(playlistExists)
            //IF you use this statement the playlistid would be zero(0) 
            //    causing your transaction to ABORT
            //Why? playlistExists.PlaylistId is currently zero.

            //INSTEAD, do the staging using the parent.navproperty.Add(xxx)
            playlistExist.PlaylistTracks.Add(playlisttrackExist);

            //Staging is complete
            //COMMIT the TRANSACTION
            //to commit work to the sql database, use .SaveChanges()
            //when doing transaction use ONE and ONLY ONE .SaveChanges()
            _context.SaveChanges();
        }
        #endregion
    }
}
