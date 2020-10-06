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

//conditional statement
if(condition)
{
	true path complelx logic
}
else
{
	false path complex logic
}

//Conditions
//			arg1 operator arg2
//relative operators
//ternery opertor
condition(s)  ? true value : false value

				
//nested ternery operator
condition(s) ? condition(s) ? true value: false 
: condition(s) ? true value : false value

//list all albums by release label. any album with no label should 
//	be indicated as unknown.list title and label

var ternaryResults	= 	from x in Albums
				orderby x.ReleaseLabel
				select new
				{
					title = x.Title,
					label = x.ReleaseLabel != null ? x.ReleaseLabel : "Unknown"
				};
	ternaryResults.Dump();	
	
//list all albums showing their title , ArtistName, 
//	and decade (Oldies,70's, 80's,90's' Modern, Order by artist


var ternaryResults2 = 	from x in Albums
						orderby x.Artist.Name
						select new
						{
							title = x.Title,
							name = x.Artist.Name,
							decade =x.ReleaseYear < 1970 ? "Oldies" :
										x.ReleaseYear < 1980 ? "70's" :
										x.ReleaseYear < 1990 ? "80's" :
										x.ReleaseYear < 2000 ? "90's" : "Modern"
						};
	ternaryResults2.Dump();	

//List all tracks indicating whether they are longer, shorter or equal to the 
//	average of all track lengths,Show Track Name and length

//examplel of using multiple queries to answer a question

//query 1, Find the average
//pre processing
var resultAverage = Tracks.Average(x => x.Milliseconds);
//Using results of query 1 in query 2
var ternaryAverage = 	from y in Tracks
						select new
						{
						title = y.Name,
						Length = y.Milliseconds < resultAverage ? "Shorter" :
									y.Milliseconds == resultAverage ? "Equal" : "Longer",
						actualLength = y. Milliseconds
						};

resultAverage.Dump();
ternaryAverage.Dump();











