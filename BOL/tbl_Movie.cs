//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_Movie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Movie()
        {
            this.tbl_ActorMovie = new HashSet<tbl_ActorMovie>();
        }
    
        public int MovieId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        [Required]
        [Range(1950, 2020)]      
        [DisplayName("Year Of Release")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Year must be numeric")]
        public int YearOfRelease { get; set; }
        [Required]
        public string Plot { get; set; }
        
        public string Poster { get; set; }
        [DisplayName("Producer Name")]
        public int ProducerId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ActorMovie> tbl_ActorMovie { get; set; }
        public virtual tbl_Producer tbl_Producer { get; set; }
    }
}