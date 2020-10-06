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

//Nested Queries
//Simply put are queries with queries

//list all sales support employees showing their full name(lastname, firstname)
//	their title and the number of customers each support.
//	Orderby fullname.
//In Addition, show a list of the customers for each employee.
//	Show the customer full name, city and state


from x in Employees
where x.Title.Contains("Support")
orderby x.LastName, x.FirstName
select new
{
	name 	= 		x.LastName + ", " +  x.FirstName,
	title 	= 		x.Title,
	//clientcount = 	x.SupportRepCustomers.Count(),
	clientcount = 	(from y in x.SupportRepCustomers
					select y).Count(),
	//clientlist = 	from y in x.SupportRepCustomers
	//				orderby y.LastName, y.FirstName
	//				select new
	//				{
	//					name = y.LastName + ", " +  y.FirstName,
	//					city = y.City,
	//					state = y.State
	//				}	
	clientlist = 	from y in Customers
	where y.SupportRepId == x.EmployeeId
					orderby y.LastName, y.FirstName
					select new
					{
						name = y.LastName + ", " +  y.FirstName,
						city = y.City,
						state = y.State
					}
}

//create a list of albums showing their title and artist. 
//show albums with 25 or more tracks only
//show the songs on the albums(name and the length


//the inner query created an IEnumerable collection
from a in Albums
where a.Tracks.Count() >= 25
select new
{
	name = a.Title,
	artist = a.Artist.Name,
	songcount = a.Tracks.Count(),
	tracklist = 	from y in a.Tracks
					select new
					{
						trackname = y.Name,
						length = y.Milliseconds
					}
}

//Create a playlist report that shows the Playlist Name, The numberof songs
//	on the playlist, the user name belonging to the playlist 
//	and the songs on the playlist with their Genre

from x in Playlists
where x.PlaylistTracks.Count() >= 20
select new
{
	name = x.Name,
	trackcount = x.PlaylistTracks.Count(),
	user = x.UserName,
	songs = 	from y in x.PlaylistTracks
				select new
				{
					track = y.Track.Name,
					genre = y.Track.Genre.Name
				}
}



















