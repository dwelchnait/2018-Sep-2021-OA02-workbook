<Query Kind="Program">
  <Connection>
    <ID>7d0a8a4c-6ce5-4be5-89ef-bd1739768e1b</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

using System;
void Main()
{
	//Use of the conversion method .ToList() on the inner query to match
	//  	the class definition given to the inner collection

  	var albumlist = Albums
					.Where(x => x.Tracks.Count >= 25)
					.Select( x => new AlbumTracksItem
					        {
								Title = x.Title,
								Artist = x.Artist.Name,
								AlbumSongs = x.Tracks
												.Select(trk => new Song
												{
													SongName = trk.Name,
													LengthInSeconds = trk.Milliseconds / 1000
												}
												).ToList()
							});
	//albumlist.Dump();

	//Using .FirstOrDefault method
	//Find an albumby Id. Check that the album exists
	//IF you wish to treat your results as a collection, then do not
	//		use the FirstOrDefault (IOrderedQueryable<T>)
	//IF you wish to treat your results as an instance of a class
	//		use the FirstOrDefault (class instance)
	int incomingparam = 1000;
	var results = Albums
					.Where(x => x.AlbumId == incomingparam)
					.Select(x => x)
					.FirstOrDefault();
	if(results == null)
	{
		//Console.WriteLine("album id does not exist");
	}
	else
	{
		//results.Dump();
	}
	
	//to remove duplicate line use .Distinct()
	
	var distinctresults = Customers
							.OrderBy(x => x.Country)
							.Select(x => x.Country)
							.Distinct();
	//distinctresults.Dump();	
	
	//Any and ALL
	//Genres.Count().Dump();
	
	//Show Genres that have tracks which are not on any playlist
	Genres
		.Where(g => g.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0))
		.Select(g => g)
		//.Dump()
		;
	
	//Show genres that have all their tracks appearing at least once
	//on a playlist. What are the popular genres?
	//Show the genre name, and list of genre tracks and number of playlists
	
	Genres
		.Where(g => g.Tracks.All(tr => tr.PlaylistTracks.Count() > 0))
		.Select(g => new
					{
						name = g.Name,
						theTracks = g.Tracks
										.Where(y => y.PlaylistTracks.Count() > 0)
										.Select(y => new
													{
														song = y.Name,
														count = y.PlaylistTracks.Count()
													})
					})
		//.Dump()
		;
		
	//Using All or Any in comparing 2 collections
	//IF you collection is NOT a complex record there is a Linq method
	//		.Except that can be used to solve your query
	
	//create a lists of tracks for two different users on the database
	//create track collection for Roberto Almeida  (user name is AlmeidaR)
	//create track collection for Michelle Brooks (user name is BrooksM)
	var almeida = PlaylistTracks
					.Where(x => x.Playlist.UserName.Contains("AlmeidaR"))
					.Select(x => new
								{
								 song = x.Track.Name,
								 genre = x.Track.Genre.Name,
								 id = x.TrackId,
								 artist = x.Track.Album.Artist.Name
								})
					.Distinct()
					.OrderBy(y => y.song)
					//.Dump()
					;
	var brooks = PlaylistTracks
					.Where(x => x.Playlist.UserName.Contains("BrooksM"))
					.Select(x => new
								{
								 song = x.Track.Name,
								 genre = x.Track.Genre.Name,
								 id = x.TrackId,
								 artist = x.Track.Album.Artist.Name
								})
					.Distinct()
					.OrderBy(y => y.song)
					//.Dump()
					;
	
	//List the tracks that BOTH Roberto and Michelle like
	//Compare 2 datasets together, data is listA that is also in listB
	//Assume listA (Roberto) and listB (Michelle)
	//listA is what you wish to report from
	//listB is the list you are comparing to
	almeida
		.Where(rob => brooks.Any(mic => mic.id == rob.id))
		.OrderBy(rob => rob.song)
		//.Dump()
		;
	
	//What songs does Roberto like but not Michelle
	almeida
		.Where(rob => !brooks.Any(mic => mic.id == rob.id))
		.OrderBy(rob => rob.song)
		//.Dump()
		;
		
	almeida
		.Where(rob => brooks.All(mic => mic.id != rob.id))
		.OrderBy(rob => rob.song)
		//.Dump()
		;	
		
	//Union
	//concatenating multiple resuts together
	//syntax (queryA).Union(queryB)[.Union(query...)]
	//rules are the same as Sql
	//	number of columns same
	//	column datatypes must match
	//  ordering should be done as a method after the last Union
	
	//List the stats of Albumson Tracks(count, cost, average track length)
	//Note: for cost and average, one will need an instance to do the aggregate
	
	//Albums with no tracks record on the database will have a count, cost and averag
	//   of zero (0)
	//
	//NOTE: if you are hard coding numeric fields, the query with the hard coded
	//			values must be the first query
	(Albums
		.Where(x => x.Tracks.Count() == 0)
		.Select(x => new
				{
					title = x.Title,
					totalTracks = 0,
					totalCost = 0.00m,
					AverageLength = 0.00
				})).Union(Albums
							.Where(x => x.Tracks.Count() > 0)
							.Select(x => new
									{
										title = x.Title,
										totalTracks = x.Tracks.Count(),
										totalCost = x.Tracks.Sum(tr => tr.UnitPrice),
										AverageLength = x.Tracks.Average(tr => tr.Milliseconds)
									}))
							.OrderBy(x => x.totalTracks)
							.Dump()
							;
}

// You can define other methods, fields, classes and namespaces here
public class Song
{
	public string SongName{get;set;}
	public int LengthInSeconds{get;set;}
}

public class AlbumTracksItem
{
	public string Title{get;set;}
	public string Artist{get;set;}
	public List<Song> AlbumSongs{get;set;}
}
