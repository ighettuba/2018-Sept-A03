<Query Kind="Statements">
  <Connection>
    <ID>b446378d-10eb-4861-86ea-1e2434c35c35</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>J4M35\SQLEXPRESS</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>GroceryList</Database>
  </Connection>
</Query>

//		EXCERCISE 2


//Query 1
var products	=	from x in Products
					orderby x.Description
					where x.OrderLists.Count() > 0
					select new
					{
						name = x.Description,
						timesPurchased =  x.OrderLists.Count()
					};
	products.Dump();
//Query 2

//Query 3

//Query 4

//Query 5
//ANY & OR
//Union....form relationship between Pickers(support file) and Orders(process file
//use join if no navigational property present



//Query 6


)