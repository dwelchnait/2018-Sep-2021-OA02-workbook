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

//Using Navigational properties and anonymous datasets

//for notes on anonymous datasets see moodle site Student Notes/
//   Demos/eRestrautant/linq pages

//Find all albums released in the 90s (1990 - 1999). 
//Order the releases by ascending year and then by album title.
//Display the Year, Title, Artist Name and Release Label.

//problem a) not all properties of Album are to be displayed
//        b) the order of the properties are to be displayed
//           in a different sequence then the definition of
//		     the properties on the entity
//        c) the artist name is in a separte entity

//solution: use an anonymous dataset

//use the object initialization syntax to create new instances
//to be produced for the resulting dataset

//the anonymous instance is defined within the .Select by
//  specifying the properties desired in the dataset

Albums   
   .OrderBy(albumitem => albumitem.ReleaseYear)
   .ThenBy(albumitem => albumitem.Title)
   .Where (albumitem => albumitem.ReleaseYear >= 1990
   				&& albumitem.ReleaseYear < 2000)
   .Select (albumitem => new
					   {
					     Year = albumitem.ReleaseYear,
						 Title = albumitem.Title,
						 Name = albumitem.Artist.Name,
						 Label = albumitem.ReleaseLabel
					   })
					   
//List all albums released in the 90s, ordered by artist, title.
//Show the artist name, title, year and label.

Albums   
	.Where (albumitem => albumitem.ReleaseYear >= 1990
   				&& albumitem.ReleaseYear < 2000)
   .OrderBy(albumitem => albumitem.Artist.Name)
   .ThenBy(albumitem => albumitem.ReleaseYear)
   
   .Select (albumitem => new
					   {
					   	 Name = albumitem.Artist.Name,
					     Year = albumitem.ReleaseYear,
						 Title = albumitem.Title,						 
						 Label = albumitem.ReleaseLabel
					   })  
			
   
   
   
   
   
   