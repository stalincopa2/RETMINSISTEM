//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RETMINSISTEM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KARDEX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KARDEX()
        {
            this.DESCRIPCION_KARDEX = new HashSet<DESCRIPCION_KARDEX>();
        }
    
        public string COD_KARDEX { get; set; }
        public Nullable<int> ID_PROVEEDOR { get; set; }
        public Nullable<short> ID_BODEGA { get; set; }
        public Nullable<int> ID_USUARIO { get; set; }
        public string ARTICULO { get; set; }
        public string UNIDADES { get; set; }
        public Nullable<double> STOCK_MIN { get; set; }
        public Nullable<double> STOCK_MAX { get; set; }
        public string LOCALIZACION { get; set; }
        public short TIPO_KARDEX { get; set; }
    
        public virtual BODEGA BODEGA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESCRIPCION_KARDEX> DESCRIPCION_KARDEX { get; set; }
        public virtual PROVEEDOR PROVEEDOR { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
