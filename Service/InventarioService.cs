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
    public class InventarioService
    {

        private List<INVENTARIO> listInvetario = new List<INVENTARIO>();
        private INVENTARIO objInventario;

     


        /*Hace el update Real en la base de datos*/
        public List<INVENTARIO> inventarioList()
            {
                String conectar = ConfigurationManager.ConnectionStrings["Model"].ConnectionString;
                SqlConnection con = new SqlConnection(conectar);
                using (con)
                {
                    using (SqlCommand inventarioList = new SqlCommand("INVETORYLIST", con))
                    {
                        inventarioList.CommandType = CommandType.StoredProcedure;
                 
                        con.Open();
                       var inventario= inventarioList.ExecuteReader();

                        while (inventario.Read())
                        {
                            objInventario = new INVENTARIO();
                            objInventario.COD_KARDEX = inventario["COD_KARDEX"].ToString();
                            objInventario.ARTICULO = inventario["ARTICULO"].ToString();
                            objInventario.UNIDADES = inventario["UNIDADES"].ToString();
                            objInventario.STOCK = inventario["STOCK"].ToString();
                            objInventario.ID_BODEGA = Convert.ToInt32(inventario["ID_BODEGA"].ToString());
                            listInvetario.Add(objInventario);
                        }
                        return listInvetario;

                    con.Close();
                    return null;
                }
                }
            }
        
    }
}