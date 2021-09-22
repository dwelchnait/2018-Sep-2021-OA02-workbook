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

//Aggregrates
//.Count(), .Max(), .Min(), .Average(), .Sum()

//IMPORTANT!!!
//AGGREGRATES WORK ONLY ON A COLLECTION OF DATA
//	NOT ON A SINGLE ROW

//aggregrates can be coded using query or method syntax
//.Count() returns the number of instances within a collection
//.Sum(), .Max(), .Min(), .Average() NEED to excute against
//	a collection of a specified single column (expression)
//  that is if you need to do one of these aggregrates you
//	need to specify what to aggregrate

//syntax
//query
//   (from .....
//     select expression).aggregrate()
//execution is 1st the query within () then the .aggregrate


//method
//  collectset.Select(x => expression).aggregrate()
//  note: for .Count() collectset.Count()
// or
//  collectset.aggregrate(x => expression)

//A collection CAN have 0, 1, or more instances
//The expression in your aggregrate of Sum, Min, Max and Average
//	CANNOT be null

//You can use multiple aggregrates in a single column

//Find the average length of track in our music collection.

//thought process
//aggregrates need collections: what is my collection?
//What is the data being aggregrated?

//collection: tracks
//data: milliseconds
//aggregrate: Average

//query syntax
(from x in Tracks
select x.Milliseconds).Average()

//method syntax
Tracks.Select(x => x.Milliseconds).Average()

//mixture of query/method syntaxfrom x in Tracks
//bad aggregate statement
//Why? 
//  x represents a single instance NOT a collection
//from x in Tracks
//select new
//{
//	AvgLength = x.Average(x => x.Milliseconds)
//}

//list all Albums of the 60s showing the title, artist
// and various aggregrates for albums containing tracks.
//for each album show the number of tracks, the longest
//track length, the shortest track length, the total price
//of the tracks, and the average track length

//query/method syntax
from x in Albums
where x.ReleaseYear > 1959 && x.ReleaseYear < 1970
		&& x.Tracks.Count() > 0
orderby x.Tracks.Count() descending
select new
{
	Title = x.Title,
	Artist = x.Artist.Name,
	NumberOfTracks = x.Tracks.Count(),
	LongestTrack = x.Tracks.Max(tr => tr.Milliseconds),
	ShortestTrack = (from tr in x.Tracks
					select tr.Milliseconds).Min(),
	TotalPrice = x.Tracks.Select(tr => tr.UnitPrice).Sum(),
	AverageTrackLength = x.Tracks.Average(tr => tr.Milliseconds)
}

//method syntax
Albums
 .Where(x => x.ReleaseYear > 1959 && x.ReleaseYear < 1970
		&& x.Tracks.Count() > 0)
 .OrderByDescending(x => x.Tracks.Count())
 .Select(x => new
	{
		Title = x.Title,
		Artist = x.Artist.Name,
		NumberOfTracks = x.Tracks.Count(),
		LongestTrack = x.Tracks.Max(tr => tr.Milliseconds),
		ShortestTrack = x.Tracks.Select(tr => tr.Milliseconds).Min(),
		TotalPrice = x.Tracks.Select(tr => tr.UnitPrice).Sum(),
		AverageTrackLength = x.Tracks.Average(tr => tr.Milliseconds)
	})






