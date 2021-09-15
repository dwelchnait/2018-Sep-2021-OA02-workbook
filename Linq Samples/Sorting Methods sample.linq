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

//OrderBy,OrderByDescending,ThenBy,ThenByDescending
//
//order your output result of the query
//
//there is a difference between query syntax and method syntax for
//   the sorting of output results
//
//query syntax
//
// orderby field [ascending,descending][, field2 [ascending,descending]]
//    ascending is the default option
//
//method syntax
// each sort request is a separate method
//
// .OrderBy(plholder => plholder) or .OrderByDescending(plholder => plholder)
// [.ThenBy(plholder => plholder) or .ThenByDescending(plholder => plholder) ...
//

//Find all albums released in the 90s (1990 - 1999). 
//Order the releases by ascending year and then by album title.
//Display the entire album record.

//often the ordering phase may be done with the word "within"
//Order the releases by album title within ascending release year.

//the sort methods NEED to be grouped

//the order of the sort methods and filter method is unimportant
Albums   
   .OrderBy(albumitem => albumitem.ReleaseYear)
   .ThenBy(albumitem => albumitem.Title)
   .Where (albumitem => albumitem.ReleaseYear >= 1990
   				&& albumitem.ReleaseYear < 2000)
   .Select (albumitem => albumitem)
   