<Query Kind="Statements">
  <Connection>
    <ID>7d0a8a4c-6ce5-4be5-89ef-bd1739768e1b</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

// Statements IDE
// you can have multiple queries written in this IDE
//    environment
// you can execute a query individually by highlighting
//    the desired query first
// BY DEFAULT executing a file in this environment executes
//    ALL queries, top to bottom
//
// IMPORTANT
// queries in this environment MUST be written using the
//   C# language grammar for a statement. This means that
//   each statement must end in a semi-colon
// results must be placed in a receiving variable
// to display the results use the Linqpad method .Dump()

//Find all albums released in 2000. Display the entire album
//record

//query syntax
var resultq = from albumitem in Albums
where albumitem.ReleaseYear == 2000
select albumitem;

resultq.Dump();

//method syntax

var resultm = Albums
   .Where (albumitem => albumitem.ReleaseYear == 2000)
   .Select (albumitem => albumitem);
   
resultm.Dump();
   
