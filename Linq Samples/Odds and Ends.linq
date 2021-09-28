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
		Console.WriteLine("album id does not exist");
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
	distinctresults.Dump();						
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
