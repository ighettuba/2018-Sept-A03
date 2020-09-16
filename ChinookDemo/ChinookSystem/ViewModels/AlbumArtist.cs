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
#endregion
namespace ChinookSystem.ViewModels
{
    public class AlbumArtist
    {
        public int AlbumId { get; set; }
        public string Title { get; set;}
        //due to the fact that this view will be used in the demonstration 
        //of a dropdown list in a gridview loaded by ODS
        //the artistid will be returned
        public int ArtistID { get; set; }
        public int ReleaseYear { get; set; }
        public string ReleaseLabel { get; set; }
    }
}
