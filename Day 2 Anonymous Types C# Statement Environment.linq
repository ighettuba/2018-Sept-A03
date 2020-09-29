<Query Kind="Statements">
  <Connection>
    <ID>e5f07e69-57d0-45d8-9f20-7bc256434fd6</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>J4M35\SQLEXPRESS</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Anonymouse Types

//This allows for creation of a query that will display data
//	not on the "from" table source AND/OR a subset
//of data from your table source

//List all tracks by AC/DC; orderdby track name.
//	Display the track name, album title, album release year
//	track length price and genre

//the default datatype for the query is eithere <IQueryable> or <IEnumerable>
//new creates another instance
//within the new coding block you will enter
//	the data that you wish returned
var results =	from x in Tracks
				orderby x.Name
				where x.Album.Artist.Name.Equals("AC/DC")
				select new
				{
					Song = x.Name,
					AlbumTitle = x.Album.Title,
					Year = x.Album.ReleaseYear,
					Length = x.Milliseconds,
					Price = x.UnitPrice,
					Genre = x.Genre.Name
				};
				
//if you are using the C# statement Environment you will place 
//	your query results into a local variable
//wou will then display your contents of the local variable
//	using the .Dump() LinqPad extension method
results.Dump();

//one does not need highlight the query to execute inf
//	there are multiple queries within the physical file
//Queries will execute from top to bottom in th e file
var results2 = 	Tracks
   					.OrderBy (x => x.Name)
   					.Where (x => x.Genre.Name.Equals ("Jazz"));
results2.Dump();