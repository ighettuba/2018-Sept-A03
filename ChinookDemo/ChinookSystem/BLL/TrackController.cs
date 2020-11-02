using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using System.ComponentModel;    //expose for ODS configuration
using ChinookSystem.ViewModels;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
#endregion
namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        public List<SongItem> Track_FindByArtist(string artistname)
        {
            using (var context = new ChinookSystemContext())
            {
				//when youare working in LinqPad, you are using Linq to sql
				//in your App you are using Linq to Entity
				//the one CHANGE you willl need to add to your query is a reference to your context DbSet<> source
				var results = from x in context.Tracks
							  orderby x.Name
							  where x.Album.Artist.Name.Equals(artistname)
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
        }
       // [DataObjectMethod(DataObjectMethodType.Select, false)]

        //public List<TrackItem> Track_List()
        //{
        //    using (var context = new ChinookSystemContext())
        //    {
        //        return context.Tracks.ToList();
        //    }
        //}

        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public TrackItem Track_Find(int trackid)
        //{
        //    using (var context = new ChinookSystemContext())
        //    {
        //        return context.Tracks.Find(trackid);
        //    }
        //}

        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public List<TrackItem> Track_GetByAlbumId(int albumid)
        //{
        //    using (var context = new ChinookSystemContext())
        //    {
        //        var results = from aRowOn in context.Tracks
        //                      where aRowOn.AlbumId.HasValue
        //                      && aRowOn.AlbumId == albumid
        //                      select aRowOn;
        //        return results.ToList();
        //    }
        //}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, string arg)
        {
            using (var context = new ChinookSystemContext())
            {
                IEnumerable<TrackList> results = null;
                if (tracksby.Equals("Artist"))
                {
                    results = from x in context.Tracks
                              where x.Album.Artist.Name.Contains(arg)

                              orderby x.Album.Artist.Name, x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                }
                else if (tracksby.Equals("MediaType"))
                {
                    int narg = int.Parse(arg);
                    results = from x in context.Tracks
                              where x.MediaTypeId== narg

                              orderby x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                }
                else if (tracksby.Equals("Genre"))
                {
                    int narg = int.Parse(arg);
                    results = from x in context.Tracks
                              where x.GenreId == narg

                              orderby x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                }
                else
                {
                    results = from x in context.Tracks
                              where x.Album.Title.Contains(arg)

                              orderby x.Album.Title, x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                }
                if (results ==null)
                {
                    return null;
                }
                else
                {
                    return results.ToList();

                }
            }
        }//eom
    }
}
