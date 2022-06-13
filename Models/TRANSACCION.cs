namespace RETMINSISTEM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRANSACCION")]
    public partial class TRANSACCION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRANSACCION()
        {
            DESCRIPCION_KARDEX = new HashSet<DESCRIPCION_KARDEX>();
        }

        [Key]
        public int ID_TRANSACCION { get; set; }

        [Required]
        [StringLength(25)]
        public string NOMBRE_TRANSACCION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESCRIPCION_KARDEX> DESCRIPCION_KARDEX { get; set; }
    }
}
