using RETMINSISTEM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;


namespace RETMINSISTEM.Service
{
    public class DKardexService
    {
        private Model db = new Model();
        private CantDisponible CantDisponible = new CantDisponible();
        private Double lastModifiedValue { get; set; } // almacena el valor del ultimo valor modificado (Aplica solo a compras)
      
        /*Me retorna todos los registros de un solo kardex, filtrado por ID*(Este metodo es aplicable unicamente para mostrar una vista filtrada por ID)*/
        public IQueryable<DESCRIPCION_KARDEX> DkardexListByIdKardex(int? id) {
            var dESCRIPCION_KARDEX = from k in db.DESCRIPCION_KARDEX
                                     where k.ID_KARDEX == id
                                     select k;
            return dESCRIPCION_KARDEX;
        }


        /*Comprueba si un kardex tiene registros ya insertados o no*/
        public bool kardexListIsEmpty(int id) {
            var dESCRIPCION_KARDEX = (from k in db.DESCRIPCION_KARDEX
                                      where k.ID_KARDEX == id
                                      select k).Take(1);// SELECCIONA SOLO EL PRIMER REGISTRO PARA EVITAR LA SELECCION DE TODOS LOS REGISTROS 

            if (dESCRIPCION_KARDEX.ToList().Count == 0) {
                return true;
            }
            return false;
        }

        public DESCRIPCION_KARDEX calcularValoresDKardex( DESCRIPCION_KARDEX dESCRIPCION_KARDEX) {

            if (dESCRIPCION_KARDEX.ID_TRANSACCION != null)//Significa que ya hay registros previos 
            {
                dESCRIPCION_KARDEX.VALOR = dESCRIPCION_KARDEX.VALOR_UNITARIO * dESCRIPCION_KARDEX.CANTIDAD;

                switch (dESCRIPCION_KARDEX.ID_TRANSACCION) {
                    case 1:// compra
                        dESCRIPCION_KARDEX.CANTIDAD_SALDO = dESCRIPCION_KARDEX.CANTIDAD_DISPONIBLE = dESCRIPCION_KARDEX.CANTIDAD; 
                    break;
                    case 2://venta
                        if (actualizarUnidadesDisponibles(dESCRIPCION_KARDEX.CANTIDAD, dESCRIPCION_KARDEX.ID_KARDEX ?? default(int)))
                            dESCRIPCION_KARDEX.CANTIDAD_SALDO = lastModifiedValue;
                    break;
                }
            }
            else // significa que es un inventario incial 
            {
                dESCRIPCION_KARDEX.CANTIDAD_DISPONIBLE = dESCRIPCION_KARDEX.CANTIDAD_SALDO; // Este es el único registro que se calcula en caso de que sea un inventario inicial, lo
            }

            dESCRIPCION_KARDEX.VALOR_SALDO = (dESCRIPCION_KARDEX.CANTIDAD_SALDO * dESCRIPCION_KARDEX.VALOR_UNITARIO);

            return dESCRIPCION_KARDEX;
        }
        
        
        /*Hay que aclarar que las unidades disponibles unicamente se actualizan cuando hay ventas,
         Si se tratara de una compra, ese valor se inserta en una nueva fila, mas no se actualiza como el caso de las ventas
        Esto es debido las reglas de metodo PEPS(FIFO) del Kardex*/

        public bool actualizarUnidadesDisponibles(Double cantidadVenta, int ID_KARDEX)
        {
            lastModifiedValue = 0;
            var dESCRIPCION_KARDEX = from dk in db.DESCRIPCION_KARDEX
                                     where dk.ID_KARDEX == ID_KARDEX && dk.CANTIDAD_DISPONIBLE > 0 && dk.ID_TRANSACCION ==1
                                     orderby dk.ID_DESCRIPCION_KARDEX
                                     select dk; // selecciona todos los registros que tengan unidades disponibles de una Kardex

            int dimensionConsulta = dESCRIPCION_KARDEX.ToList().Count();
            if (dimensionConsulta == 0)
            {
                return false; // significa que no hay registros con unidades disponibles disponibles            
            }

            int i = 0;
            Double? auxTotal;// variable que calcula el valor total al restarlo por la cantidad de venta en cada registro

            foreach (var item in dESCRIPCION_KARDEX) {
                auxTotal = item.CANTIDAD_DISPONIBLE - cantidadVenta;

                if (auxTotal < 0)
                {
                    item.CANTIDAD_DISPONIBLE = 0; // si es menor que cero primero resta todo el stock disponible en ese registro y la parte negativa se reasigna a cantidad venta como lo que sobra de la resta
                    cantidadVenta = (auxTotal ?? default(Double)) * -1; // convierte de Double? a Double y lo multiplica por (-1) para convertirlo en positivo y poderlo restar con el siguiente registro                           
                }
                else {
                    item.CANTIDAD_DISPONIBLE = auxTotal;
                    lastModifiedValue = item.CANTIDAD_DISPONIBLE ?? default(Double);
                    CantDisponible.updateCantDisponible(item);
                    return true;
                   }
                CantDisponible.updateCantDisponible(item);
                    i++;
                }                   
            return false;// retorna falso por defecto, signficai que
                 // si no es de tipo venta devuleve la misma cantidad de Saldo, la cantidad de saldo es la misma ya que esta tiene que quedar registrada.
        }

        public void restaurarUnidadesDisponibles(int ID_DESCRIPCION, int ID_KARDEX) {// solo ocurrre en caso de modificación, restaura los valores que estaban diponibles en cada registro

            double? cantidadAux=0;
            var kardexesActuales = (from k in db.DESCRIPCION_KARDEX
                                   where k.ID_KARDEX == ID_KARDEX 
                                   select k).OrderByDescending(K=> K.ID_DESCRIPCION_KARDEX) ;
            
            foreach (var item in kardexesActuales) {
                if (item.ID_DESCRIPCION_KARDEX == ID_DESCRIPCION)
                {
                    cantidadAux = item.CANTIDAD;
                }
                else if (item.ID_TRANSACCION == 1) {

                    item.CANTIDAD_DISPONIBLE = (cantidadAux + item.CANTIDAD_DISPONIBLE);

                    if (item.CANTIDAD_DISPONIBLE > item.CANTIDAD)
                    {
                        cantidadAux = item.CANTIDAD_DISPONIBLE - item.CANTIDAD;
                        item.CANTIDAD_DISPONIBLE -= cantidadAux;
                    }
                    else {
                        item.CANTIDAD_DISPONIBLE = cantidadAux;
                        CantDisponible.updateCantDisponible(item);
                        break;
                    }
                    CantDisponible.updateCantDisponible(item);
                
                }          
            }
        }

        public DESCRIPCION_KARDEX modificarRegistros(DESCRIPCION_KARDEX dESCRIPCION_KARDEX) {

            if (dESCRIPCION_KARDEX.ID_TRANSACCION != null)//Significa que ya hay registros previos 
            {
                restaurarUnidadesDisponibles(dESCRIPCION_KARDEX.ID_DESCRIPCION_KARDEX, dESCRIPCION_KARDEX.ID_KARDEX ?? default(int)); // primero restaura los valores anteriores, con el algoritmo respectivo

                dESCRIPCION_KARDEX.VALOR = dESCRIPCION_KARDEX.VALOR_UNITARIO * dESCRIPCION_KARDEX.CANTIDAD;

                switch (dESCRIPCION_KARDEX.ID_TRANSACCION)
                {
                    case 1:// compra
                        dESCRIPCION_KARDEX.CANTIDAD_DISPONIBLE = dESCRIPCION_KARDEX.CANTIDAD;
                        dESCRIPCION_KARDEX.CANTIDAD_SALDO = dESCRIPCION_KARDEX.CANTIDAD_DISPONIBLE;
                        break;
                    case 2://venta
                        if (actualizarUnidadesDisponibles(dESCRIPCION_KARDEX.CANTIDAD, dESCRIPCION_KARDEX.ID_KARDEX ?? default(int))) // se vuleven a calcular los valores, despues de que ya se restauraron los anteriores.
                            dESCRIPCION_KARDEX.CANTIDAD_SALDO = lastModifiedValue;
                        break;
                }
            }
            else // significa que es un inventario incial 
            {
                dESCRIPCION_KARDEX.CANTIDAD_DISPONIBLE = dESCRIPCION_KARDEX.CANTIDAD_SALDO; // Este es el único registro que se calcula en caso de que sea un inventario inicial, lo
            }

            dESCRIPCION_KARDEX.VALOR_SALDO = (dESCRIPCION_KARDEX.CANTIDAD_SALDO * dESCRIPCION_KARDEX.VALOR_UNITARIO);

            return dESCRIPCION_KARDEX;

        }

    }
}