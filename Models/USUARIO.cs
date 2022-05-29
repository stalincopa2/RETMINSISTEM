namespace RETMINSISTEM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USUARIO")]
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            DESCRIPCION_KARDEX = new HashSet<DESCRIPCION_KARDEX>();
            KARDEX = new HashSet<KARDEX>();
        }

        [Key]
        public int ID_USUARIO { get; set; }

        public int ID_ROL { get; set; }

        [Required]
        [StringLength(10)]
        public string COD_USUARIO { get; set; }

        [Required]
        [StringLength(10)]
        public string CEDULA { get; set; }

        [Required]
        [StringLength(100)]
        public string NOMBRE { get; set; }

        [Required]
        [StringLength(50)]
        public string APELLIDO { get; set; }

        [Column("USUARIO")]
        [Required]
        [StringLength(18)]
        public string USUARIO1 { get; set; }

        [Required]
        [MaxLength(800)]
        public byte[] CONTRACENIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESCRIPCION_KARDEX> DESCRIPCION_KARDEX { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KARDEX> KARDEX { get; set; }

        public virtual ROL ROL { get; set; }
    }
}
