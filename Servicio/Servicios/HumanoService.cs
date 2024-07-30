using Data;
using Microsoft.EntityFrameworkCore;
using Modelos.Modelos;
using Servicio.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Servicios
{
    public class HumanoService : IHumanoService
    {

        private readonly AppDBContext _db;

        public HumanoService(AppDBContext db)
        {
            _db = db;
        }

        public async Task Actualizar(Humano humano)
        {
            var humanoTemp = await ObtenerHumano(humano.Id);
            if(humanoTemp == null)
                throw new TaskCanceledException("El Humano no Existe");

            humanoTemp.Nombre = humano.Nombre;
            humanoTemp.Altura = humano.Altura;
            humanoTemp.Edad = humano.Edad;
            humanoTemp.Peso = humano.Peso;
            humanoTemp.Sexo = humano.Sexo;

            await _db.SaveChangesAsync();
        }

        public async Task<Humano> Agregar(Humano humano)
        {
            _db.Humanos.Add(humano);
            await _db.SaveChangesAsync();
            return humano;
        }

        public async Task<bool> HumanoExiste(string Nombre)
        {
            return await _db.Humanos.AnyAsync(humano => humano.Nombre.ToLower() == Nombre.ToLower());
        }

        public async Task<Humano> ObtenerHumano(int id)
        {
            var humano = await _db.Humanos.FindAsync(id);
            return humano;
        }

        public async Task<IEnumerable<Humano>> ObtenerTodos()
        {
            var lista = await _db.Humanos.ToListAsync();
            return lista;
        }
    }
}
