namespace RETMINSISTEM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KARDEX")]
    public partial class KARDEX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KARDEX()
        {
            DESCRIPCION_KARDEX = new HashSet<DESCRIPCION_KARDEX>();
        }

        [Key]
        public int ID_KARDEX { get; set; }

        
        [StringLength(10)]
        public string COD_KARDEX { get; set; }

        public int ID_PROVEEDOR { get; set; }

        public int ID_BODEGA { get; set; }

        public int ID_USUARIO { get; set; }

        public int ID_TIPO_KARDEX { get; set; }

        [Required]
        [StringLength(100)]
        public string ARTICULO { get; set; }

        [Required]
        [StringLength(20)]
        public string UNIDADES { get; set; }

        public double? STOCK_MIN { get; set; }

        public double? STOCK_MAX { get; set; }

        [Required]
        [StringLength(100)]
        public string LOCALIZACION { get; set; }

        [Column(TypeName = "image")]
        public byte[] FOTO_ARTICULO { get; set; }

        public virtual BODEGA BODEGA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESCRIPCION_KARDEX> DESCRIPCION_KARDEX { get; set; }

        public virtual PROVEEDOR PROVEEDOR { get; set; }

        public virtual TIPO_KARDEX TIPO_KARDEX { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
