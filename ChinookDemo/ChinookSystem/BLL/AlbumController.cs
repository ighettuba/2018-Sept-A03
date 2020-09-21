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
     public class AlbumController
    {
        #region Queries
        //return all album records
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
                                  ArtistId = x.ArtistID,
                                  ReleaseYear = x.ReleaseYear,
                                  ReleaseLabel = x.ReleaseLabel
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]

        public List<AlbumList> Album_List(int artistid)
        {
            using (var context = new ChinookSystemContext())
            {
                var results = from x in context.Albums
                              select new AlbumList
                              {
                                  AlbumId = x.AlbumId,
                                  Title = x.Title,
                                  ArtistId = x.ArtistID,
                                  ReleaseYear = x.ReleaseYear,
                                  ReleaseLabel = x.ReleaseLabel
                              };
                return results.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]

        public AlbumList Album_FindByID(int albumid)
        {
            ///rerturn album record matching supply pkey or null
            using (var context = new ChinookSystemContext())
            {
                var results = (from x in context.Albums
                              where x.AlbumId == albumid
                              select new AlbumList
                              {
                                  AlbumId = x.AlbumId,
                                  Title = x.Title,
                                  ArtistId = x.ArtistID,
                                  ReleaseYear = x.ReleaseYear,
                                  ReleaseLabel = x.ReleaseLabel
                              }).FirstOrDefault();
                return results;
            }
        }
        #endregion
        #region CRUD methods ADD, Update, Delete
        [DataObjectMethod(DataObjectMethodType.Insert,false)]
        public void Album_Add(AlbumList item)
        {
            using(var context = new ChinookSystemContext())
            {
                //moving the data from the external view model instance
                //int an internal instance of the entity
                Album addItem = new Album
                {
                    Title = item.Title,
                    ArtistID = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };
                context.Albums.Add(addItem);
                context.SaveChanges(); //causes entity validation to run
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Album_Update(AlbumList item)
        {
            using (var context = new ChinookSystemContext())
            {
                //moving the data from the external view model instance
                //int an internal instance of the entity
                Album updateItem = new Album
                {
                    AlbumId = item.AlbumId,
                    Title = item.Title,
                    ArtistID = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };
                context.Entry(updateItem).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges(); //causes entity validation to run
            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Album_Delete(AlbumList item)
        {
            Album_Delete(item.AlbumId);           
        }


        public void Album_Delete(int albumID)
        {
            using (var context = new ChinookSystemContext())
            {
                var exists = context.Albums.Find(albumID);
                context.Albums.Remove(exists);
                context.SaveChanges(); //causes entity validation to run
            }
        }
        #endregion
    }
}
