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

//UNION

//will combine 2 or more queries into one result
//each query need to have the same number of columns
//the query should have the same associated data within the column
//each query column needs to be the same datatype between queries

//syntax
// (query1).union(query2). union(queryN). OrderBy(first sort).TneyBy(nth sort)
//sorting is done using the column name from the union

//Generate a report covering all albums showing their title
//	thie track count, the album price, and the average track length
//	Order by the number of tracks on the album then by album title

//remember datatypes of columns must match
//Sum added up a decimal field, Average returns a double
//Albums.Count().Dump();

var unionresults =  (from x in Albums
					where x.Tracks.Count() > 0
					select new
					{
						title = x.Title,
						trackcount = x.Tracks.Count(),
						albumprice = x.Tracks.Sum(y => y.UnitPrice),
						averagelength = x.Tracks.Average(y => y.Milliseconds/1000)						
					}).Union(	from x in Albums
								where x.Tracks.Count() == 0
								select new
								{
									title = x.Title,
									trackcount = 0,
									albumprice = 0.00m,
									averagelength = 0.0						
								}).OrderBy(y => y.trackcount).ThenBy(y => y.title);
//unionresults.Dump();


//Joins

////www.dotnetlearners.com/linq

//AVOID JOINS if tehre is an acceptable navigational property available
//joins can be use where navigational properties DO NOT EXIST
//joins can be used between associated entities
//		scenario FKey <==> PKey

//left side of the join, use the support data
//rightside of the join, use Processing record collection

//unfortunatelly, Chinook entities are all navigational property setup
//assume there is NO navigational property between artist and album

//syntax
//supportside join processside on supportkey == processkey

//in our question the support => artist and process => album

var joinResults	=	from supportside in Artists
					join processside in Albums
					on supportside.ArtistId equals processside.ArtistId
					select new
					{
						title = processside.Title,
						year = processside.ReleaseYear,
						label = processside.ReleaseLabel == null ? "Unknown" : processside.ReleaseLabel,
						artist = supportside.Name,
						trackcount = processside.Tracks.Count()
					};
joinResults.Dump();














































