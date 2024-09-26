using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Problema1_5.Entities;
using Problema1_5.Services;

namespace Problema1_5WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
         private readonly FacturaService _facturaService;

        public FacturasController()
        {
            _facturaService = new FacturaService();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = _facturaService.GetAll();
            return Ok(list);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult Get(int id)
        {
            var art = _facturaService.GetById(id);
            if (art != null)
            {
                return Ok(art);

            }
            return NotFound();
        }
        [HttpGet("DateOrPayment")]
        public IActionResult GetByDateOrPayment(DateTime? date = null, int? payment= null)
        {
            var art = _facturaService.GetByPaymentOrDate(date,payment);
            if (art != null)
            {
                return Ok(art);

            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Factura factura)
        {
            _facturaService.Save(factura);
            return Ok(factura);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Factura factura)
        {
            var result = _facturaService.Update(factura);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _facturaService.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

