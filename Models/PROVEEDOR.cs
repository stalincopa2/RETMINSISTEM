namespace RETMINSISTEM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PROVEEDOR")]
    public partial class PROVEEDOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROVEEDOR()
        {
            KARDEX = new HashSet<KARDEX>();
        }

        [Key]
        public int ID_PROVEEDOR { get; set; }

 
        [StringLength(10)]
        public string COD_PROOVEDOR { get; set; }

        [Required]
        [StringLength(50)]
        public string NOMBRE_PROVEEDOR { get; set; }

        [Required]
        [StringLength(50)]
        public string APELLIDO_PROVEEDOR { get; set; }

        [Required]
        [StringLength(150)]
        public string EMPRESA_REPRESENTA { get; set; }

        [StringLength(10)]
        public string CONTACTO { get; set; }

        [StringLength(100)]
        public string CORREO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KARDEX> KARDEX { get; set; }
    }
}
