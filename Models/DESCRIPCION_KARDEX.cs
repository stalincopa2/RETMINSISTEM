namespace RETMINSISTEM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DESCRIPCION_KARDEX
    {
        [Key]
        public int ID_DESCRIPCION_KARDEX { get; set; }

        public int? ID_KARDEX { get; set; }

        public int? ID_USUARIO { get; set; }

        public DateTime FECHA_KARDEX { get; set; }

        [Column("DESCRIPCION_KARDEX")]
        [Required]
        [StringLength(100)]
        public string DESCRIPCION_KARDEX1 { get; set; }

        [Required]
        [StringLength(1)]
        public string TIPO_TRANSACION { get; set; }

        public double VALOR_UNITARIO { get; set; }

        public double CANTIDAD { get; set; }

        public double VALOR { get; set; }

        public double? CANTIDAD_SALDO { get; set; }

        public double? VALOR_SALDO { get; set; }

        public DateTime? CADUCIDAD { get; set; }

        public virtual KARDEX KARDEX { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
