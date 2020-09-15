using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
#region Addition Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion
namespace ChinookSystem.Entities
{ 
    //Annotate your entity to link to the sql table
        //                      to indicate primary key and key type
        //                      to include validation on fields
    //Applies to the field that follow anotation only
    [Table("Artists")]
    internal class Artist
    {

        //private data member
        private string _Name;
        //properties
        //the annotation for a primary key is [Key ] indicated the field is an identity primary key
        //an option on this annotation call DataBaseF-Generated(DataBaseGeneratedOption.xxxx)
        // Where xxxx is Identity, Computed or None
        //Computed indicated attribute is not real data but a computed field from your database
        //none is use on a PK that the user MUST supply

        [Key]
        //          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        //        [Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int ArtistId { get; set; }

        //[Required(ErrorMessage ="Name is Required")]          for required fields
        [StringLength(120,ErrorMessage ="Name is limited to 120 characters")]
        public string Name
        {
            get { return _Name; }
                set{ _Name = string.IsNullOrEmpty(value) ? null : value; }
        }

        // Navigational Properties
        //the relationship in Artist is parent to child(1:m)
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
        //constructor area

        //behaviour area
    }
}
