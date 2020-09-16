using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using System.ComponentModel;
using ChinookSystem.ViewModels;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
#endregion
namespace ChinookSystem.BLL
{
     public class AlbumController
    {
        public List<AlbumArtist> Album_FindByArtist(int artistid)
        {
            using (var context = new ChinookSystemContext())
            {
                var results = from x in context.Albums
                              where x.ArtistID == artistid
                              select new AlbumArtist
                              {
                                  AlbumId = x.AlbumId,
                                  Title = x.Title,
                                  ArtistID = x.ArtistID,
                                  ReleaseYear = x.ReleaseYear,
                                  ReleaseLabel = x.ReleaseLabel
                              };
                return results.ToList();
            }
        }
    }
}
