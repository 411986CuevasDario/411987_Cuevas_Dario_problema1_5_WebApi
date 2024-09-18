using Microsoft.Data.SqlClient;
using Problema1_5.Entities;
using Problema1_5.Utils;
using System;
using System.Collections.Generic;
using System.Data;


namespace Problema1_5.Data
{
    public interface IFormaPagoRepository
    {
        List<FormaPago> GetAll();
        bool Save(FormaPago formaPago);
        bool Delete(int id);
    }
    public class FormaPagoRepository : IFormaPagoRepository
    {
        public List<FormaPago> GetAll()
        {
            var dh = DataHelper.GetInstance().ExecuteSPQuery("obtener_formas_pagos");
            var list = new List<FormaPago>();
            foreach (DataRow c in dh.Rows)
            {
                var fp = new FormaPago();
                fp.Id = (int)c["id"];
                fp.Nombre = c["nombre"].ToString();
                list.Add(fp);
            }

            return list;
        }
        public bool Save(FormaPago formaPago)
        {
            SqlCommand command = new SqlCommand("crear_formapago");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@forma", formaPago.Nombre));


            return DataHelper.GetInstance().ExecuteSPQuery(command);
        }
        public bool Delete(int id)
        {
            return false;
        }
    }
}


