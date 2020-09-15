using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Addition Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
#endregion
namespace ChinookSystem.Entities
{
    [Table("Tracks")]
    internal class Track
    {
        //properties
        [Key]
        public int TrackId { get; set; }

        [StringLength(200, ErrorMessage = "Name is limited to 200 characters")]
        [Required(ErrorMessage ="Track Name is required")]
        public string Name { get; set; } 
        public int? AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int GenreId { get; set; }

        [StringLength(220, ErrorMessage = "Composer is limited to 120 characters")]
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }

        //Navigational Properties
        public virtual Album Album { get; set; }
        public virtual MediaType MediaType { get; set; }
    }
}
