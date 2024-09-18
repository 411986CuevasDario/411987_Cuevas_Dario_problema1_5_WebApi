using Problema1_5.Data;
using Problema1_5.Entities;
using Problema1_5.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_5.Services
{
    public class FormaPagoService
    {
        private readonly IFormaPagoRepository _formaPagoRepository;

        public FormaPagoService()
        {
            _formaPagoRepository = new FormaPagoRepository();
        }
        public List<FormaPago> GetAll()
        {
            return _formaPagoRepository.GetAll(); 
        }
        public bool Save(FormaPago forma)
        {
            return _formaPagoRepository.Save(forma);
        }
        public bool Delete(int id)
        {
            return _formaPagoRepository.Delete(id);

        }
    }
}
