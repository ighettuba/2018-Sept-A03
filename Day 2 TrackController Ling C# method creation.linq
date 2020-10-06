<Query Kind="Program">
  <Connection>
    <ID>e5f07e69-57d0-45d8-9f20-7bc256434fd6</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>J4M35\SQLEXPRESS</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{

	//if you are using the C# statement Environment you will place 
	//	your query results into a local variable
	//wou will then display your contents of the local variable
	//	using the .Dump() LinqPad extension method
	//one does not need highlight the query to execute inf
	//	there are multiple queries within the physical file
	//Queries will execute from top to bottom in th e file
	
	//in the program environment you can define classes and methods
	
	//execute a method
	var results = BLL_Query("AC/DC");
	results.Dump();
}
//VIEWMODEL CLASS
// Define other methods, classes and namespaces here
public class SongItem
{
	public string Song{get; set;}
	public string AlbumTitle{get; set;}
	public int Year{get; set;}
	public int Length{get; set;}
	public decimal Price{get; set;}
	public string Genre{get; set;}
}

//BLL CLASS
//create a method to simulate the BLL method
public List<SongItem> BLL_Query(string artistName)
{
	//change the Anonymous Datatype to a trongly-typed datatype
	//define a class and use it with the new
	var results =	from x in Tracks
					orderby x.Name
					where x.Album.Artist.Name.Equals(artistName)
					select new SongItem
					{
						Song = x.Name,
						AlbumTitle = x.Album.Title,
						Year = x.Album.ReleaseYear,
						Length = x.Milliseconds,
						Price = x.UnitPrice,
						Genre = x.Genre.Name
					};
	return results.ToList();				
}








