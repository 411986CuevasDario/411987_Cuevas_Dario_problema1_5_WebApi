

using Microsoft.Data.SqlClient;
using Problema1_5.Entities;
using Problema1_5.Utils;
using System.Data;

namespace Problema1_5.Data
{
    public interface IArticuloRepository 
    {
        List<Articulo> GetAll();
        bool Save(Articulo cliente);
        bool Delete(int cliente);
        Articulo GetById(int id);
        bool Update(Articulo art);
    }
    public class ArticuloRepository : IArticuloRepository
    {
        public List<Articulo> GetAll()
        {
            var dh = DataHelper.GetInstance().ExecuteSPQuery("obtener_articulos");
            var list = new List<Articulo>();
            foreach (DataRow c in dh.Rows)
            {
                var art = new Articulo();
                art.Id = (int)c["id"];
                art.Nombre = c["nombre"].ToString();
                art.PrecioUnitar = Decimal.Parse(c["precioUnitar"].ToString());
                
                list.Add(art);
            }
            return list;
            
        }
        public bool Save(Articulo art)
        {
            SqlCommand command = new SqlCommand("crear_articulo");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@nombre", art.Nombre));
            command.Parameters.Add(new SqlParameter("@precio", art.PrecioUnitar));


            return  DataHelper.GetInstance().ExecuteSPQuery(command);

        }
        public bool Update(Articulo art)
        {
            SqlCommand command = new SqlCommand("modificar_articulo");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@nombre", art.Nombre));
            command.Parameters.Add(new SqlParameter("@id", art.Id));

            command.Parameters.Add(new SqlParameter("@precio", art.PrecioUnitar));


            return DataHelper.GetInstance().ExecuteSPQuery(command);

        }
        public bool Delete(int id) { 

            DataHelper.GetInstance().GetById("eliminar_articulo",id);

            return true;
        }
        public Articulo GetById(int id) {
            try
            {
                var dh = DataHelper.GetInstance().GetById("obtener_articulo_id", id);
                var art = new Articulo
                {
                    Id = (int)dh.Rows[0]["ID"],
                    Nombre = dh.Rows[0]["nombre"].ToString(),
                    PrecioUnitar = (decimal)dh.Rows[0]["PrecioUnitar"]
                };
                return art;
            }
            catch (Exception)
            {

                return null;
            }
          
        }
    }
}
