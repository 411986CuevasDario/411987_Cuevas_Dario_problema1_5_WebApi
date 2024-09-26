using Microsoft.AspNetCore.Mvc;
using Problema1_5.Entities;
using Problema1_5.Services;


namespace Problema1_5WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly ArticuloService _articuloService;

        public ArticulosController()
        {
            _articuloService = new ArticuloService();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = _articuloService.GetAll();
            return Ok(list);
        }

        [HttpGet("/GetById/{id}")]
        public IActionResult Get(int id)
        {
            var art = _articuloService.GetById(id);
            if (art != null)
            {
                return Ok(art);

            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Articulo articulo)
        {
            _articuloService.Save(articulo);
            return Ok(articulo);
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Articulo articulo)
        {
            var result = _articuloService.Update(articulo);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _articuloService.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
