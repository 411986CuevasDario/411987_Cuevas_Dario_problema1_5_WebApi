
using Problema1_5.Data;
using Problema1_5.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_5.Services
{
    public class FacturaService
    {
        private readonly IFacturaRepository _facturaRepository;

      public FacturaService()
        {
            _facturaRepository = new FacturaRepository();
        }
        public List<Factura> GetAll()
        {
            return _facturaRepository.GetAll(); 
        }
        public Factura GetById(int id)
        {
            return _facturaRepository.GetById(id);
        }
        public bool Save(Factura cliente)
        {
            return _facturaRepository.Save(cliente);
        }
        public bool Delete(int id)
        {
            return _facturaRepository.Delete(id);

        }
    }
}
