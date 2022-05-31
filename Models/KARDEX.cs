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
        [Key]
        public short ID_KARDEX { get; set; }

        public int? ID_PROVEEDOR { get; set; }

        public short? ID_BODEGA { get; set; }

        public int? ID_USUARIO { get; set; }

        [Required]
        [StringLength(10)]
        public string COD_KARDEX { get; set; }

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

        public short TIPO_KARDEX { get; set; }

        [Column(TypeName = "image")]
        public byte[] FOTO_ARTICULO { get; set; }
        /*
        [Column(TypeName = "text")]
        public string foto { get; set; }
        */
      
        public virtual BODEGA BODEGA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESCRIPCION_KARDEX> DESCRIPCION_KARDEX { get; set; }

        public virtual PROVEEDOR PROVEEDOR { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
