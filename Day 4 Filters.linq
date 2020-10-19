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
select x.Country).Distinct();

var distinctMethodResults = Customers
   .OrderBy (x => x.Country)
   .Select (x => x.Country)
   .Distinct ();
   
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
//anyResults.Dump();
//sometimes you have two lists that need to be compare
//Usually you are looking for items that are in the same (in both collections) OR
//			you are looking for items that are different
//in either case: you are comparing one collection to a second collection

//we are going to compare the playlists for  2 individuals on the database

//obtaion a distinct list of all playlist tracks of Roberto Almeida (user Almeida)

var almeida = 	(from x in PlaylistTracks
				where x.Playlist.UserName.Contains("Almeida")
				select new
				{
					song = x.Track.Name,
					genre =x.Track.Genre.Name,
					id = x.TrackId
				}).Distinct().OrderBy(x => x.song);
//			almeida.Dump();

//obtaion a distinct list of all playlist tracks of Michelle Brooks (user BrooksM)

var brooks = 	(from x in PlaylistTracks
				where x.Playlist.UserName.Contains("Brooks")
				select new
				{
					song = x.Track.Name,
					genre =x.Track.Genre.Name,
					id = x.TrackId
				}).Distinct().OrderBy(x => x.song);
//			brooks.Dump();


//start with the comparison
//list the tracks that both Roberto and Michele like
//I find it best to think of the collections as A and B
//think of processing as for each A, check to see if it is any of B

var likes 	= 	almeida
				.Where(a => brooks.Any(b => b.id == a.id))
				.OrderBy(a => a.song)
				.Select(a => a);
//	likes.Dump();

//differences
//list Robertos tracks that michelle doesnt have
//find records in collection A NOT in collection B
var almeidaDiff 	= 	almeida
				.Where(a => !brooks.Any(b => b.id == a.id))
				.OrderBy(a => a.song)
				.Select(a => a);
	almeidaDiff.Dump();

var brooksDiff 	= 	brooks
				.Where(a => !almeida.Any(b => b.id == a.id))
				.OrderBy(a => a.song)
				.Select(a => a);
//brooksDiff.Dump();


//All() method iterates through the entire collection to see 
//		all items that match the condition
//returns true or false
//an instance of the collection that recieves a true on oteh condition
//     is for processing

////show genres that have all their tracks appearing atleast onces on 
//a play list

var genretotal = Genres.Count();
genretotal.Dump();

var popularGenres 	= 	from x in Genres
						where x.Tracks.All(trk => trk.PlaylistTracks.Count() > 0)
						orderby x.Name
						select new
						{
							genre = x.Name,
							genretracks = x.Tracks.Count(),
							theTracks	=	from y in x.Tracks
											where y.PlaylistTracks.Count()>0
											select new
											{
												song =y.Name
												
											}
						};
	popularGenres.Dump();	
										
					














