using RETMINSISTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RETMINSISTEM.Service
{
    public class DKardexService
    {
        private Model db = new Model();

        public IQueryable<DESCRIPCION_KARDEX> DkardexListByIdKardex(int? id ) {
            var dESCRIPCION_KARDEX = from k in db.DESCRIPCION_KARDEX
                                     where k.ID_KARDEX == id
                                     select k;     
            return dESCRIPCION_KARDEX;
        }
        public bool kardexListIsEmpty(int id) {
            var dESCRIPCION_KARDEX = (from k in db.DESCRIPCION_KARDEX
                                     where k.ID_KARDEX == id
                                     select  k).Take(1);// SELECCIONA SOLO EL PRIMER REGISTRO PARA EVITAR LA SELECCION DE TODOS LOS REGISTROS 

            if (dESCRIPCION_KARDEX.ToList().Count==0) {
                return true;
            }
                return false; 
        }
    }
}