<Query Kind="Expression">
  <Connection>
    <ID>e5f07e69-57d0-45d8-9f20-7bc256434fd6</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>J4M35\SQLEXPRESS</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

////comments are enterd as C#

//hotkeys for comments
//Ctrl + K,C make coment
//Ctrl + K,U uncoment

//there are two styles of coding linq queries
//Query Syntax (very sql-ish)
//Method Syntax (very C#-ish)

//in the expression environmnet, you can code multiple queries
// 	BUT you must highlight the query to execute (F5)

//in the statement environment, you can code multiple queries
// 	as C# statements and run the entire physical file without
// 	highlighting the query

//in the program environment, you can codemultiple queries
// 	AND class definitions or program methods which are tested
//	in a Main() program

//simple selection with a sort
//Query Syntax Expression of a query
//FROM clause is !st
//SELECT clause is last
from x in Albums
orderby x.Title ascending
select x

//Method Syntax of a query
Albums.OrderBy (x => x.Title)

from x in Albums
orderby x.ReleaseYear descending,x.Title ascending
select x

Albums
   .OrderByDescending (x => x.ReleaseYear)
   .ThenBy (x => x.Title)
   
//filtering of data
//where clasue
//list artists with a Q in their name
from x in Artists
where x.Name.Contains("Q")
select x

//show all Albums released in the 90's
from x in Albums
where (x.ReleaseYear > 1989) && (x.ReleaseYear < 2000)
select x

//list all customers in alphabetic order by last name
//	who live in the USA.The customer must have a yahoo email
from x in Customers
orderby x.LastName ascending
where x.Country.Equals("USA") && x.Email.Contains("yahoo")
//where x.Country.Contains("USA")
//where x.Country == "USA"
select x








