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

//show all albums for U2, order by  year, by title

//a demontration of using the navigational properties
//	to access properties of another table
//Query syntax
from album in Albums
orderby album.ReleaseYear, album.Title
where album.Artist.Name.Equals("U2")
select album

//Method syntax
Albums
   .OrderBy (album => album.ReleaseYear)
   .ThenBy (album => album.Title)
   .Where (album => album.Artist.Name.Equals ("U2"))


//List alll jazz tracks by Name
//query
from x in Tracks
orderby x.Name
where x.Genre.Name.Equals("Jazz")
select x

//Method syntax
Tracks
   .OrderBy (x => x.Name)
   .Where (x => x.Genre.Name.Equals ("Jazz"))


//List all tracks for AC/DC
from x in Tracks

where x.Album.Artist.Name.Equals("AC/DC")
select x