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

void Main()
{
	string partialSongName = "Dance";
	//var results = SongsByPartialName(partialSongName);
	//results.Dump(); //Linqpad method
	
	var songCollection = Tracks
						.Where(x => x.Name.Contains(partialSongName))
						.OrderBy(x => x.Album.Title)
						.ThenBy(x => x.Name)
						.Select(x => new SongList
								{
								  Album = x.Album.Title,
								  Song = x.Name,
								  Artist = x.Album.Artist.Name
								});
	songCollection.Dump();
}

// You can define other methods, fields, classes and namespaces here

List<SongList> SongsByPartialName(string partialSongName)
{
	var songCollection = Tracks
						.Where(x => x.Name.Contains(partialSongName))
						.OrderBy(x => x.Album.Title)
						.ThenBy(x => x.Name)
						.Select(x => new SongList
								{
								  Album = x.Album.Title,
								  Song = x.Name,
								  Artist = x.Album.Artist.Name
								});
	return songCollection.ToList();
}

public class SongList
{
	public string Album{get;set;}
	public string Song{get;set;}
	public string Artist{get;set;}
}
