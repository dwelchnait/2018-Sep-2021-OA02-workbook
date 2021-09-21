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

//Find any Track of music that has the word Big in its name.
//Show the track name, the album title, and the artist.
//Order the display by the album title then the track name.

Tracks
	.Where(x => x.Name.Contains("Big"))
	.OrderBy(x => x.Album.Title)
	.ThenBy(x => x.Name)
	.Select(x => new
			{
			  Album = x.Album.Title,
			  Song = x.Name,
			  Artist = x.Album.Artist.Name
			})