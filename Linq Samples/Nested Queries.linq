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
	//Nested Queries
	//sometimes referred to as subqueries
	
	//simply put: it is a query within a query [....]
	
	//List all sales support employees showing their
	//fullname (last, first), title and the number of 
	//customers each supports. Order by fullname.
	//In addition, show a list of the customers for each
	//employee. List the customer fullname, phone, city
	//and state. Order the customers by state, city.
	
	//there will be 2 separate lists with the same final
	//	dataset collection
	//one for employees
	//one for each employee
	
	//C# point of view in a class definition
	//classname
	//    property
	//    property 
	// 	  ...
	//    collection<T> (set of records)
	
	//nested queries can be coded using both query syntax
	//and method syntax
	
	//query
	var resultsq = from emp in Employees
					where emp.Title.Contains("Sales Support")
					orderby emp.LastName, emp.FirstName
					select new
					{
						FullName = emp.LastName + ", " +
									emp.FirstName,
						Title = emp.Title,
						NumberOfCustomers = emp.SupportRepCustomers.Count(),
						CustomerList = from cus in emp.SupportRepCustomers
									   orderby cus.State, cus.City
									   select new
									   {
									   		State = cus.State == null ? " " :
														cus.State,
											City = cus.City,
											Name = cus.LastName + ", " +
													cus.FirstName,
											Phone = cus.Phone
									   }
					};
	//resultsq.Dump();
	
	//method syntax
	var resultsm = Employees
				   .Where (emp => emp.Title.Contains ("Sales Support"))
				   .OrderBy (emp => emp.LastName)
				   .ThenBy (emp => emp.FirstName)
				   .Select (emp => 
						         new  EmployeeItem
						         {
						            FullName = ((emp.LastName + ", ") + 
												emp.FirstName), 
						            Title = emp.Title, 
						            NumberOfCustomers = emp.SupportRepCustomers.Count (),
									CustomerList = emp.SupportRepCustomers
													.OrderBy(cus => cus.State)
													.ThenBy (cus => cus.City)
													.Select (cus => new CustomerItem
																{
																	State = cus.State == null ?
																	    " " : cus.State,
																	City = cus.City,
																	Name = cus.LastName + ", " +
																			cus.FirstName,
																	Phone = cus.Phone
																}
																)
						         }
						   );
	resultsm.Dump();

//Create a list of Albums showing its title and artist.
//Show albums with 25 or more tracks only. ***
//Show the songs on the album listing the name and song length in seconds.
//(There are 1000 milliseconds in a second)

}

//You can define other methods, fields, classes and namespaces here

//declare a class representing the Employee record from the query
public class EmployeeItem
{
	public string FullName{get;set;}
	public string Title{get;set;}
	public int NumberOfCustomers{get;set;}
	public IEnumerable<CustomerItem> CustomerList{get;set;}
}

//declare a class representing the Customer record from the query
public class CustomerItem
{
	public string Name{get;set;}
	public string Phone {get;set;}
	public string City {get;set;}
	public string State {get;set;}
}
