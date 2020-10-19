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
	//Create a playlist report that shows the Playlist Name, The numberof songs
	//	on the playlist, the user name belonging to the playlist 
	//	and the songs on the playlist with their Genre
	
	//lowestplaylistsize is going to be a parameter when this query is implemented in my application
	var lowestplaylistsize = 20;
	var results =	from x in Playlists
					orderby x.UserName
					where x.PlaylistTracks.Count() >= lowestplaylistsize
					select new PlayListItem
					{
						Name = x.Name,
						TrackCount = x.PlaylistTracks.Count(),
						UserName= x.UserName,
						Songs = 	from y in x.PlaylistTracks
									orderby y.Track.Genre.Name, y.Track.Name
									select new PlayListSong
									{
										Song = y.Track.Name,
										GenreName = y.Track.Genre.Name
									}
					};
	results.Dump();
}

// You can define other methods, fields, classes and namespaces here
public class PlayListSong
{
	public string Song{get; set;}
	public string GenreName{get; set;}
	
}

public class PlayListItem
{
	public string Name{get;set;}
	public int TrackCount{get;set;}
	public string UserName{get;set;}
	public	IEnumerable<PlayListSong> Songs{get;set;}
}