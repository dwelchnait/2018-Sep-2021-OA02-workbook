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

//Grouping

//when you create a group it builds two (2) components
// a) Key component (groug by)
//    reference this component using a groupname.Key[.attribute]
// b) data of the group (instances in the group)

//ways to group
//a) by a single column (field, attribute, property)	groupname.Key
//b) by a set of column (anonymous dataset)				groupname.Key.Attribute
//c) by using an entity									groupname.Key.Attribute

//concept processing
//start with a "pile" of data (original collection)
//specify the grouping attribute(s)
//result of the group will be to "place the data into smaller piles"
//   the piles are dependent on the grouping attribute(s) 
//   the grouping attribute(s) become the Key
//   the individual instances are the data in the smaller piles
//   the entire individual instance is place in the smaller pile
//manipulate each of the "smaller piles" using your linq commands

//grouping is different the Ordering
//Ordering is the re-sequencing of a collection for display
//grouping re-organizes a collection in separate, usually smaller.
//    collections for processing

//Display albums by ReleaseYear
//this request does not need grouping
//this request is an ordering of output: OrderBy
Albums
	.OrderBy(x => x.ReleaseYear)


//Display albums grouped by ReleaseYear
Albums
	.GroupBy(x => x.ReleaseYear)

//Display albums grouped by ReleaseYear. Display the ReleaseYear
//and the number of albums released.
Albums
	.GroupBy(x => x.ReleaseYear)
	.Select(eachgPile => new
	{
		Year = eachgPile.Key,
		NumberOfAlbums = eachgPile.Count()
	})
	
//query syntax
from x in Albums
group x by x.ReleaseYear into eachgPile
select new
{
	Year = eachgPile.Key,
	NumberOfAlbums = eachgPile.Count()
}

//grouping using multiple columns
//you will create a new anonymous class for use within your query
//	this class will be your Key 
//this temporary class DOES NOT need to have a phyiscal class code


//Display albums grouped by ReleaseLabel, ReleaseYear. Display the ReleaseYear
//and the number of albums released. List only the years with 5
//or more albums releases

//method
Albums
	.GroupBy(x => new {x.ReleaseLabel, x.ReleaseYear})
	.Where(eachgPile => eachgPile.Key.ReleaseLabel != null &&
					eachgPile.Count() > 2)
	//.OrderBy(eachgPile => eachgPile.Key.ReleaseLabel)
	.Select(eachgPile => new
	{
		Label = eachgPile.Key.ReleaseLabel,
		//NumberOfAlbums = eachgPile.Count()
		Year = eachgPile.Key.ReleaseYear,
		AlbumItems = eachgPile
						.Select(eachgPileRecord => new 
						{
							//YearOnAlbum = eachgPileRecord.ReleaseYear,
							TitleOnAlbum = eachgPileRecord.Title
						})
	})
	.OrderBy(y => y.Label)

//timing of the OrderBy is dependent on the statement location
//IF the OrderBy is done AFTER the .Select, the OrderBy uses the
//	data collection from the .Select
//IF the OrderBy is done BEFORE the .Select, the OrderBy is using
//	the small piles of grouped data and thus must be based on
//	said collection

//entity grouping





