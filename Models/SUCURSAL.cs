namespace RETMINSISTEM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SUCURSAL")]
    public partial class SUCURSAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUCURSAL()
        {
            BODEGA = new HashSet<BODEGA>();
            USUARIO = new HashSet<USUARIO>();
            VEHICULO = new HashSet<VEHICULO>();
        }

        [Key]
        public int ID_SUCURSAL { get; set; }

        [Required]
        [StringLength(13)]
        public string RUC_SUCURSAL { get; set; }

       
        [StringLength(10)]
        public string COD_SUCURSAL { get; set; }

        [Required]
        [StringLength(100)]
        public string NOMBRE_SUCURSAL { get; set; }

        [Required]
        [StringLength(150)]
        public string DIRECCION { get; set; }

        [Required]
        [StringLength(10)]
        public string TELEFONO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BODEGA> BODEGA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO> USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VEHICULO> VEHICULO { get; set; }
    }
}
