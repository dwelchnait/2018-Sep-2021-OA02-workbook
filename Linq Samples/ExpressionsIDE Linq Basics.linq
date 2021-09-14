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

// Our code is using C# grammar/syntax
//
// comments are done with slashes
// hotkeys  make line comment ctrl+k,ctrl+c
//          uncomment ctrl+k, ctrl+u
// alternate is to use ctrl + / as a toggle
//
// Expressions
// single linq query statements without a semi-colon
// you can have multiple statements in your file BUT
//     if you do, you MUST highlight the statement to
//     execute.
//
//execute using F5 or the green triangle on the query menu
//
// to toggle your results on and off (visible) use ctrl + R
//
//query syntax
// uses a "sql-like" syntax
// view the Student Notes to examples under Notes, Linq Intro

//Find all albums released in 2000. Display the entire album
//record
from albumitem in Albums
where albumitem.ReleaseYear == 2000
select albumitem

//method syntax
//uses C# method syntax OOP language grammar
// The collection Albums
// to excute a method of the collection you need to use
//    the access operator (dot operator)
// method name start with a capital
// methods contain contents with a delegate
// a delegate describes the action to be done
Albums
   .Where (albumitem => albumitem.ReleaseYear == 2000)
   .Select (albumitem => albumitem)
   
