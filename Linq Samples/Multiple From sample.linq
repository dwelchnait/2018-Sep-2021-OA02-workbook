<Query Kind="Expression">
  <Connection>
    <ID>7d0a8a4c-6ce5-4be5-89ef-bd1739768e1b</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//reporting between "grandparent" and "grandchild" without the "parent"

//report from Albums and PlaylistTracks but not from Track

//one possible way of dong the "join" is to physical include a where clause
// 	similar to the tableA inner join tableB on condition in sql

//This limits and confinds the optimization that Linq and Sql can create
//It works but you shoulld FIRST ALWAYS consider using navigation propeties
//		before doing your own join conditions

//List all albums (title) of the 70's with the number of songs that
//   exists on album. List the song, the playlist name, and
//	 the owner of the playlist.

//query syntax
from alb in Albums
where alb.ReleaseYear > 1969 && alb.ReleaseYear < 1980
select new
{
	title = alb.Title,
	albumtrackcount = alb.Tracks.Count(),
	playlistssongs = from tr in alb.Tracks
						from pltrk in tr.PlaylistTracks
						select new
						{
							song = pltrk.Track.Name,
							playlist = pltrk.Playlist.Name,
							owner = pltrk.Playlist.UserName
						}
}

//method syntax
Albums
   .Where (alb => ((alb.ReleaseYear > 1969) && (alb.ReleaseYear < 1980)))
   .Select (
      alb => 
         new  
         {
            title = alb.Title, 
            albumtrackcount = alb.Tracks.Count (), 
            playlistssongs = alb.Tracks
               .SelectMany (
                  tr => tr.PlaylistTracks, 
                  (tr, pltrk) => 
                     new  
                     {
                        song = pltrk.Track.Name, 
                        playlist = pltrk.Playlist.Name, 
                        owner = pltrk.Playlist.UserName
                     }
               )
         }
   )





