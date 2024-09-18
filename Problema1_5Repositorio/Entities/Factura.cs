using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_5.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } 
        public int FormaPagoId { get; set; }  
        public FormaPago FormaPago { get; set; }
        public List<DetalleFactura> DetallesFacturas { get; set; }  = new List<DetalleFactura>();  

        public string Cliente { get; set; }

        public void AgregarDetalle(Articulo articulo, int cantidad)
        {
            var detalleExistente = DetallesFacturas.FirstOrDefault(d => d.Articulo.Id == articulo.Id);
            if (detalleExistente != null)
            {
                detalleExistente.Cantidad += cantidad;
            }
            else
            {
                DetallesFacturas.Add(new DetalleFactura { Articulo= articulo ,ArticuloId = articulo.Id,Cantidad =cantidad });
            }
        }
        public override string ToString()
        {
            string detalles = string.Join(", ", DetallesFacturas?.Select(d => d.ToString()) ?? new List<string>());
            return $"Factura: Id={Id}, Fecha={Fecha}, FormaPagoId={FormaPagoId}, FormaPago={FormaPago?.ToString()}, Cliente={Cliente}, DetallesFacturas=[{detalles}]";
        }
    }
}
