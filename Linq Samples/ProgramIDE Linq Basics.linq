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
	// Program IDE
	// you can have multiple queries written in this IDE
	//    environment
	// This environment works "like" a console application
	//
	// This allows one to pre-test complete components that can
	//    be move directly into your application (class library)
	//
	// IMPORTANT
	// queries in this environment MUST be written using the
	//   C# language grammar for a statement. This means that
	//   each statement must end in a semi-colon
	// results must be placed in a receiving variable
	// to display the results use the Linqpad method .Dump()
	
	//Find all albums released in 2000. Display the entire album
	//record
	int inParam = 2000; //simulates the incoming method parameter
	
	var resultm = Albums
	   .Where (albumitem => albumitem.ReleaseYear == inParam)
	   .Select (albumitem => albumitem);
	   
	resultm.Dump();
	//return resultm.ToList();
}

// You can define other methods, fields, 
// classes and namespaces here






