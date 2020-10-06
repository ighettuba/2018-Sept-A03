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

//Aggregates

//.Count(), .Sum(), .Max(), .Min(), .Average()

//.Sum(), .Max(), .Min(), .Average() require a delegate expression

// Query syntax
// 	(from......
//		......
//	).Max()

//Method Syntax
//	collection.Max(.x => x.collectionfield)
//collectionfield could also be a calculation
//	.Sum(x => x.quantity * x.price)


//IMPORTANT!!!!!!!
//aggegates work ONLY on a collection of data
//		NOT on a single row

//A collection CAN have 0,1 or more rows
// the delegate of .Sum(), .Max(), .Min(), .Average() CANNOT be null
//. Count() does not need a delegate, it counts occurances

//bad example of using aggegate
//aggregate is against a single row
from x in Tracks
select new
{
	Name = x.Name,
	AvgLength = x.Average(x => x.Milliseconds) //wrong, single row
}

//GOOD EXAMPLE
//the list of all milliseconds in Tracks is created THEN the aggegate 
//		is applied
(from x in Tracks
select x.Milliseconds //wrong, single row
).Average()

Tracks.Average(x=>x.Milliseconds)

//List all albums showing the title, artist name and various aggregate values
//for albums containing tracks. For Each Album, 
//Show the number of tracks
//the longest track length
//the shortest track length
//totla price of the tracks
//the average track length
from x in Albums
where x.Tracks.Count() > 1
select new
{
	TrackTitle 		= 	x.Title,
	ArtistName 		= 	x.Artist.Name,
	methodcount 	= 	x.Tracks.Count(),
	//querycount = 	(from y in x.Tracks 
	//						select x).Count(),
	querycount 		= 	(from y in Tracks 
							where x.AlbumId == y.AlbumId
							select x).Count(),
	AvgLength 		= 	x.Tracks.Average(y => y.Milliseconds),
	MaxLength 		= 	x.Tracks.Max(y => y.Milliseconds),
	MinLength 		= 	x.Tracks.Min(y => y.Milliseconds),
	AlbumPrice 		= 	x.Tracks.Sum(y => y.UnitPrice)
}




