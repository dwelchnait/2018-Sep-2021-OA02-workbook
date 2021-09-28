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

//group based on an entity


//Create a query which will report the employee and their customer base.
//List the employee fullname (phone), number of customer in their base.
//List the fullname, city and state for the customer base.

//You pobably would want to attack this question by using the navigational
//  properties, and you would be correct.
//However, this example will show that the same answer can be obtained
//  using grouping

//how to attack this question using grouping
//tips:
//What is the detail of the query? What is reported on most?
//			Customer base (big pile of data)
//Is the report one complete report or is it in smaller components?
//			order by vs group by
//Can I subdivide (group) my details into specific piles? If so, on what?
//			group by Employee (smaller piles of data based on xxxxxx)
//Is there an association between Customers and Employees?
//			nav property SupportRep

//once you have done your group clause, on the select, you are processing
//  ONE small pile of records at a time
//the select will process each small pile of records in your collection
//  before moving on to the next pile

Customers
	.GroupBy(x => x.SupportRep)
	//.Select(gTemp => gTemp)
	.Select(gTemp => new
			{
				Employee = gTemp.Key == null ? "Unassigned" :
							gTemp.Key.LastName + ", " +
							gTemp.Key.FirstName + " (" +
							gTemp.Key.Phone + ")",
				NumOfCust = gTemp.Count(),
				CustomerList = gTemp
								.Select(cus => new
											{
												CustName = cus.LastName +
												  ", " + cus.FirstName,
												 City = cus.City,
												 State = cus.State
											})
							
			})
	
	
	
	
	
	
	
	
	
	
	
	
	











