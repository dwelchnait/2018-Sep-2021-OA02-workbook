using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
#endregion


namespace ChinookSystem.BLL
{
    public class PlaylistTrackServices
    {
        #region Constructor and Dependency Injection fields
        private readonly ChinookContext _context;
        //todo:
        // - get the Database context
        // - Chinookcontext is internal
        // - thus constructor needs to be internal for access match
        internal PlaylistTrackServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public List<PLTrackInfo> GetPLTracksforUser(string playlistname, string username)
        {

            IEnumerable<PLTrackInfo> info = _context.PlaylistTracks
                            .Where(x => x.Playlist.Name.Equals(playlistname) 
                                     && x.Playlist.UserName.Equals(username))
                            .Select(x => new PLTrackInfo
                            {
                                TrackId = x.TrackId,
                                TrackNumber = x.TrackNumber,
                                Name = x.Track.Name,
                                Milliseconds = x.Track.Milliseconds
                                
                            })
                            .OrderBy(x => x.TrackNumber);
            
            return info.ToList();
        }
        #endregion

        #region Maintain Playlist
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            Playlist playlistExists = null;
            PlaylistTrack playlisttrackExists = null;
            int tracknumber = 0;
           
                //This class is in what is called the Business Logic Layer
                //Business Logic is the rules of your business
                //  rule: a track can only exist once on a playlist
                //  rule: each track on a playlist is assigned a continious
                //        track number
                //
                //The BLL method should also ensure that data exists for
                //   the processing of the transaction
                if (string.IsNullOrEmpty(playlistname))
                {
                    //there is a data error
                    
                    throw new Exception("Playlist name is missing. Unable to add track");
                }
                if (string.IsNullOrEmpty(username))
                {
                    //there is a data error
                    throw new Exception("User name was not supplied");
                }

                //does the playlist exist?
                playlistExists = (from x in _context.Playlists
                                  where (x.Name.Equals(playlistname)
                                      && x.UserName.Equals(username))
                                  select x).FirstOrDefault();
                if (playlistExists == null)
                {
                    //the playlist DOES NOT exists
                    //tasks:
                    //      create a new instance of a playlist object
                    //      load the instance with data
                    //      stage the add of the new instance
                    //      set a variable representing the tracknumber to 1
                    playlistExists = new Playlist()
                    {
                        Name = playlistname,
                        UserName = username
                    };
                    _context.Playlists.Add(playlistExists); //stage ONLY!!!!!!!!!!
                    tracknumber = 1;
                }
                else
                {
                    //the playlist already exists
                    //verify track not already on playlist (business rule)
                    //what is the next tracknumber
                    //add 1 to the tracknumber
                    playlisttrackExists = (from x in _context.PlaylistTracks
                                           where x.Playlist.Name.Equals(playlistname)
                                               && x.Playlist.UserName.Equals(username)
                                               && x.TrackId == trackid
                                           select x).FirstOrDefault();
                    if (playlisttrackExists == null)
                    {
                        tracknumber = (from x in _context.PlaylistTracks
                                       where x.Playlist.Name.Equals(playlistname)
                                           && x.Playlist.UserName.Equals(username)
                                       select x).Count();
                        tracknumber++;
                    }
                    else
                    {
                        throw new Exception("Track already on playlist.");
                    }
                }

                //create the playlist track
                playlisttrackExists = new PlaylistTrack();

                //load of the playlist track
                playlisttrackExists.TrackId = trackid;
                playlisttrackExists.TrackNumber = tracknumber;

                //??????
                //what is the playlist id
                //if the playlist exists then we know the id
                //BUT if the playlist is new, we DO NOT know the id

                //in one case the id is known BUT in the second case
                //    where the new record is ONLY STAGED, NO primary key
                //    value has been generated yet.
                //if you access the new playlist record the pkey would be 0 (default numeric)

                //the solution to BOTH of these scenarios is to use
                //    navigational properties during the ACTUAL .Add command
                //    for the new playlisttrack record
                //the entityframework will, on your behave, ensure that the adding
                //      of records to the database will be done in the appropriate
                //      order AND will add the missing compound primary key value
                //      (PlaylistId) to the new playlisttrack record

                //NOT LIKE this!!!! THIS IS WRONG!!!!!
                //context.PlaylistTracks.Add(playlisttrackExists);

                //INSTEAD, do the staging using the parent.navproperty.Add(xxxx)
                playlistExists.PlaylistTracks.Add(playlisttrackExists);

                
                    //COMMIT THE TRANSACTION
                    //the ALL the staged records to sql for processing
                    _context.SaveChanges();
                
        }//eom


        public void DeleteTracks(string playlistname, string username, List<CTrackInfo> trackstodelete)

        {
            //Vaidation data presents
            if (string.IsNullOrEmpty(playlistname))
            {
                throw new Exception("Playlist name is missing. Unable to add track");
            }
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("User name was not supplied");
            }
            //count number of tracks selected for removal
            //rule: must have at least one selected

            //this is the test using a method declared in the record
            if (trackstodelete.Where(x => x.Checked()).Select(x => x.TrackId).Count() == 0)
            {
                throw new Exception("You did not select any tracks to delete");
            }

            //this is the test using the datafield in the record
            if (trackstodelete.Where(x => x.InputData != null).Select(x => x.TrackId).Count() == 0)
            {
                throw new Exception("You did not select any tracks to delete");
            }


            //get Playlist instance
            Playlist exists = (from x in _context.Playlists
                                where x.Name.Equals(playlistname)
                                    && x.UserName.Equals(username)
                                select x).FirstOrDefault();
            //does playlist exist
            //rule: if no playlist exists then there can be no playlist tracks
            if (exists == null)
            {
                throw new Exception("Playlist does not exist");
            }

            //remove the desired tracks
            //     11, 235, 34, ...
            PlaylistTrack item = null;
            foreach (var deletetrack in trackstodelete)
            {
                item = _context.PlaylistTracks
                                .Where(tr => tr.Playlist.Name.Equals(playlistname)
                                        && tr.Playlist.UserName.Equals(username)
                                        && tr.TrackId == deletetrack.TrackId
                                        && deletetrack.InputData != null)
                                .Select(tr => tr).FirstOrDefault();
                if (item != null)
                {
                    //staged
                    //    parent.navproperty.Remove()
                    exists.PlaylistTracks.Remove(item);
                }
            }

            //re - sequence the kept tracks
            
            int tracknumber = 1;
            foreach (var track in trackstodelete)
            {

                //list of all track that are to be kept
                var trackskept = _context.PlaylistTracks
                                    .Where(tr => tr.Playlist.Name.Equals(playlistname)
                                            && tr.Playlist.UserName.Equals(username)
                                             && tr.TrackId == track.TrackId
                                            && track.InputData == null)
                                    .Select(tr => tr).FirstOrDefault();
                if (trackskept != null)
                {
                    trackskept.TrackNumber = tracknumber;
                    _context.Entry(trackskept).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;  //Staged
                    tracknumber++;
                }
            }

            //save the work

            _context.SaveChanges();
            
        }//eom

        #endregion
    }//eoc
}

