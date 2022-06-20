using RETMINSISTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RETMINSISTEM.Service
{
    public class CantDisponible
    {

        /*Hace el update Real en la base de datos*/
        public void updateCantDisponible(DESCRIPCION_KARDEX dKardex)
        {

            String conectar = ConfigurationManager.ConnectionStrings["Model"].ConnectionString;
            SqlConnection con = new SqlConnection(conectar);
            using (con)
            {
                using (SqlCommand updateCantidades = new SqlCommand("UPDATECANTIDADDISPONIBLES", con))
                {
                    updateCantidades.CommandType = CommandType.StoredProcedure;
                    updateCantidades.Parameters.Add("@ID_DESCRIPCION_KARDEX", SqlDbType.Int).Value = dKardex.ID_DESCRIPCION_KARDEX;
                    updateCantidades.Parameters.Add("@ID_KARDEX", SqlDbType.Int).Value = dKardex.ID_KARDEX;
                    updateCantidades.Parameters.Add("@CANTIDAD_DISPONIBLE", SqlDbType.Float).Value = dKardex.CANTIDAD_DISPONIBLE;
                    con.Open();
                    updateCantidades.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}