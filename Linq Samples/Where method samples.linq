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

//Where
//filter method
//the conditions are setup as you would in C#
//beware that Linqpad may not like some C# syntax (DateTime)
//beware that Linq is converted to SQL which may not
//		like certain C# syntax because SQL could convert
//
//.Where(pholder => condition [logical operator condition2 ...])

//Find all albums released in 2000. Display the entire album
//record

Albums
   .Where (albumitem => albumitem.ReleaseYear == 2000)
   .Select (albumitem => albumitem)
   
//Find all albums released in the 90s (1990 - 1999). Display the entire album
//record

Albums
   .Where (albumitem => albumitem.ReleaseYear >= 1990
   				&& albumitem.ReleaseYear < 2000)
   .Select (albumitem => albumitem)

//Find all the albums of the artist Queen.
//concern: the artist name is in another table
//         in an sql Select query you would be using a Join
//         in Linq you DO NOT need to specific your Join, instead
//	           use the "navigational properties" of your entity
//				to generate the relationship

Albums
	.Where(x => x.Artist.Name.Contains("Queen"))
	.Select(x => x)


