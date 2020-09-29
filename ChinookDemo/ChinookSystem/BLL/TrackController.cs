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
    }
}
