using Modelos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Servicios.Interfaces
{
    public interface IHumanoService
    {
        Task<IEnumerable<Humano>> ObtenerTodos();
        Task<Humano> ObtenerHumano(int id);
        Task<Humano> Agregar(Humano humano);
        Task Actualizar(Humano humano);
        Task<bool> HumanoExiste(string Nombre);
    }
}
