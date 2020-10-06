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

//.Distinct()
//Createe a list of customer Countries

var distinctResults = (from x in Customers
orderby x.Country
select x.Country).Distinct()

var distinctMethodResults = Customers
   .OrderBy (x => x.Country)
   .Select (x => x.Country)
   .Distinct ()
   
//boolean filteres .Any() and .All()

//	.Any() method iterates through the entire collection.
//If anny of the items match the specified conditions,
//	true is returned.
//Boolean filters return NO data, just TRUE or FALSE
//An instance of the collection that recieves a true on the condition is selected for processing.

//show genres which have tracks not on any playlist

var anyResults = 	from  x in Genres
					where x.Tracks.Any(trk => trk.PlaylistTracks.Count() == 0)
					orderby x.Name
					select new
					{
						genre = x.Name,
						tracksingenre = x.Tracks.Count(),
						boringtracks = from y in x.Tracks
						where y.PlaylistTracks.Count() == 0
						select y.Name
					};
					anyResults.Dump();






















