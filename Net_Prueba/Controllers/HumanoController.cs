using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos;
using Servicio.Servicios.Interfaces;
using System.Security.Cryptography;

namespace Net_Prueba.Controllers
{
    public class HumanoController : BaseAPIController
    {
        private readonly IHumanoService _humanoService;

        public HumanoController(IHumanoService humanoService)
        {
            _humanoService = humanoService;
        }

        [HttpGet("Lista")]
        public async Task<ActionResult<IEnumerable<Humano>>> GetHumanos()
        {
            var Humanos = await _humanoService.ObtenerTodos();
            return Ok(Humanos);
        }

        [HttpGet("Obtener/{id}")]
        public async Task<ActionResult<Humano>> GetHumano(int id)
        {
            var humano = await _humanoService.ObtenerHumano(id);
            return Ok(humano);
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<Humano>> Agregar(Humano humano) 
        { 
            if(await _humanoService.HumanoExiste(humano.Nombre))
                return BadRequest("El Humano ya existe");

            await _humanoService.Agregar(humano);
            return Ok(humano);
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar(Humano humano)
        {
            await _humanoService.Actualizar(humano);
            return Ok(humano);
        }

        [HttpGet("ListaMock")]
        public async Task<ActionResult<IEnumerable<Humano>>> GetHumanosMock()
        {
            var humanos = new List<Humano>
            {
                new Humano { Id = 1, Nombre = "Brandon Silva Reyes", Sexo = 'M', Edad = 30, Altura = 175, Peso = 70 },
                new Humano { Id = 2, Nombre = "Anya Areli Hernandez Ramirez", Sexo = 'F', Edad = 25, Altura = 165, Peso = 55 },
                new Humano { Id = 3, Nombre = "Yulan Hernandez Ramirez", Sexo = 'M', Edad = 40, Altura = 180, Peso = 80 },
                new Humano { Id = 4, Nombre = "Alicia Luna Vega", Sexo = 'F', Edad = 35, Altura = 170, Peso = 60 }
            };

            return Ok(humanos);
        }
    }
}
