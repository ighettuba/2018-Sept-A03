using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Addition Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion
namespace ChinookSystem.Entities
{
    [Table("Albums")]           //Table annotation
    internal class Album        //internal class access
    {
        //private data members
        private string _Label;

        //properties
        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage ="Title of Album is required")]
        [StringLength(160,ErrorMessage = "Album Title can not be more than 160 characters in length")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Artist ID is required")]
        //[FOREIGN KEY]         DO NOT USE!!!!!!!!!!!!!!!
        public int ArtistID { get; set; }

        [Required(ErrorMessage = "Year album was released is required")]
        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "Release Label can not be more than 50 characters in length")]
        public string ReleaseLabel
        {
            get { return _Label; }
            set { _Label = string.IsNullOrEmpty(value) ? null : value; }
        }

        //Navigational Properties
        //used to overlay a model of the DB ERD relationships
        //you need to know the ERD Relationship between Table A and Table B
        //we have a relationship betwin artist and album
        //that relationship parent  (Artist) to child (Album)
        
        //the relationship in Album is child to parent(1:1)
        public virtual Artist Artist { get; set; }
        

        //constructors

        //methods
    }
}
