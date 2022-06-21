using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RETMINSISTEM.Models
{
    public class INVENTARIO
    {
       public String COD_KARDEX { get; set; }
       public String ARTICULO { get; set; }
       public String UNIDADES { get; set; }
       public String STOCK { get; set; }
       public int ID_BODEGA { get; set; }

    }
}