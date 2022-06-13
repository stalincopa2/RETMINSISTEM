namespace RETMINSISTEM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VEHICULO")]
    public partial class VEHICULO
    {
        [Key]
        public int ID_VEHICULO { get; set; }

        public int ID_SUCURSAL { get; set; }

       
        [StringLength(10)]
        public string COD_VEHICULO { get; set; }

        [StringLength(50)]
        public string MARCA { get; set; }

        [StringLength(100)]
        public string MODELO { get; set; }

        [StringLength(25)]
        public string COLOR { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHA_ADQUISISION { get; set; }

        [StringLength(250)]
        public string UBIC_ACTUAL { get; set; }

        public virtual SUCURSAL SUCURSAL { get; set; }
    }
}
