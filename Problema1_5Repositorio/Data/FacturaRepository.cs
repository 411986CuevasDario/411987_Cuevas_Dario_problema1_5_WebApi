
using Microsoft.Data.SqlClient;
using Problema1_5.Entities;
using Problema1_5.Utils;
using System.Data;

namespace Problema1_5.Data
{
    public interface IFacturaRepository 
    {
        List<Factura> GetAll();
        bool Save(Factura factura);
        bool Delete(int id);
        Factura GetById(int id);
        bool Update(Factura factura);
    }
    public class FacturaRepository : IFacturaRepository
    {
       
        public List<Factura> GetAll()
        {
            var dh = DataHelper.GetInstance().ExecuteSPQuery("obtener_facturas");
            var list = new List<Factura>();
            foreach (DataRow c in dh.Rows)
            {
                var factura = new Factura();
                factura.Id = (int)c["id"];
                factura.FormaPagoId = (int)c["FormaPagoId"];
                factura.Fecha = DateTime.Parse(c["Fecha"].ToString());
                factura.Cliente = c["Cliente"].ToString();

                list.Add(factura);
            }
            return list;
        }
        public Factura GetById(int id)
        {
            var dh = DataHelper.GetInstance().GetById("obtener_factura_id",id);
            var factura = new Factura();
            factura.Id = (int)dh.Rows[0]["facturaId"];
            factura.Fecha = DateTime.Parse(dh.Rows[0]["Fecha"].ToString());
            factura.Cliente = dh.Rows[0]["Cliente"].ToString();
            factura.DetallesFacturas = new List<DetalleFactura>();
            factura.FormaPago = new FormaPago();
            factura.FormaPago.Id = (int)dh.Rows[0]["formapagoid"];
            factura.FormaPago.Nombre = dh.Rows[0]["nombre"].ToString(); 
            foreach (DataRow c in dh.Rows)
            {
                var df = new DetalleFactura();
                df.Id = (int)c["detalleid"];
                df.ArticuloId = (int)c["articuloid"];
                df.Cantidad = (int)c["cantidad"];

                df.Articulo.Nombre = c["articulonombre"].ToString();


                factura.DetallesFacturas.Add(df);
            }
            return factura;
        }
        public bool Save(Factura factura)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction ts = null;
            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                ts = cnn.BeginTransaction();

                SqlCommand command = new SqlCommand("crear_factura",cnn, ts);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@cliente", factura.Cliente));
                command.Parameters.Add(new SqlParameter("@fecha", factura.Fecha));
                command.Parameters.Add(new SqlParameter("@formapago", factura.FormaPagoId));

                SqlParameter parm = new SqlParameter("id", SqlDbType.Int);
                parm.Direction = ParameterDirection.Output;
                command.Parameters.Add(parm);

                command.ExecuteNonQuery();

                foreach(var detalle in factura.DetallesFacturas)
                {
                    SqlCommand command1 = new SqlCommand("crear_detalle", cnn, ts);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.Add(new SqlParameter("@articuloId", detalle.ArticuloId));
                    command1.Parameters.Add(new SqlParameter("@cantidad", detalle.Cantidad));
                    command1.Parameters.Add(new SqlParameter("@facturaId",parm.Value));
                 

                    command1.ExecuteNonQuery();

                }
                ts.Commit();

            }
            catch (SqlException)
            {

               if(ts != null)
                {
                    ts.Rollback();
                }
                    result = false;
            }
            finally
            {
                if(cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
           

        }
        public bool Update(Factura factura)
        {
            SqlCommand command = new SqlCommand("modificar_factura ");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@id", factura.Id));
            command.Parameters.Add(new SqlParameter("@pagoId", factura.FormaPagoId));
            command.Parameters.Add(new SqlParameter("@fecha", factura.Fecha));
            command.Parameters.Add(new SqlParameter("@cliente", factura.Cliente));




            return DataHelper.GetInstance().ExecuteSPQuery(command);

        }
        public bool Delete(int id)
        {

            DataHelper.GetInstance().GetById("eliminar_factura", id);

            return true;
        }
    }
}
