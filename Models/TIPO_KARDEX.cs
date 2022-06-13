namespace RETMINSISTEM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TIPO_KARDEX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPO_KARDEX()
        {
            KARDEX = new HashSet<KARDEX>();
        }

        [Key]
        public int ID_TIPO_KARDEX { get; set; }

        [Required]
        [StringLength(50)]
        public string NOMBRE_TIPO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KARDEX> KARDEX { get; set; }
    }
}
