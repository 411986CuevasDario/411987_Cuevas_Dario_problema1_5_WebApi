using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_5.Entities
{
    public class DetalleFactura
    {
        public  int Id { get; set; }
        public int FacturaId {  get; set; } 
        public int ArticuloId {  get; set; }
        public Articulo Articulo { get; set; } = new Articulo();
        public int Cantidad { get; set; }
        public override string ToString()
        {
            return $"DetalleFactura: Id={Id}, FacturaId={FacturaId}, ArticuloId={ArticuloId}, Articulo={Articulo?.ToString()}, Cantidad={Cantidad}";
        }
    }
}
