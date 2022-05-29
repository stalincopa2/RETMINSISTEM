namespace RETMINSISTEM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BODEGA")]
    public partial class BODEGA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BODEGA()
        {
            KARDEX = new HashSet<KARDEX>();
        }

        [Key]
        public short ID_BODEGA { get; set; }

        public int ID_SUCURSAL { get; set; }

        [Required]
        [StringLength(10)]
        public string COD_BODEGA { get; set; }

        [Required]
        [StringLength(150)]
        public string UBICACION { get; set; }

        [StringLength(250)]
        public string DESCRIPCION_BODEGA { get; set; }

        public virtual SUCURSAL SUCURSAL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KARDEX> KARDEX { get; set; }
    }
}
