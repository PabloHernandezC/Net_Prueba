using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Net_Prueba.Controllers
{
    public class OperacionesController : BaseAPIController
    {
        [HttpPost]
        public IActionResult OperacionPost(string operacion, decimal num1, decimal num2)
        {
            operacion = operacion.ToLower();

            if (!EsOperacionBasica(operacion))
                return BadRequest("La operacion no es una operacion basica(Suma, Resta, Multiplicacion, Division)");

            decimal resultado = Operaciones(operacion,num1,num2);

            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult OperacionGet([FromHeader(Name = "Numero1")] string numero1Header,
                                 [FromHeader(Name = "Numero2")] string numero2Header,
                                 [FromHeader(Name = "Operacion")] string operacionHeader)
        {
            if (!decimal.TryParse(numero1Header, out decimal num1) ||
                !decimal.TryParse(numero2Header, out decimal num2) ||
                !operacionHeader.Any())
                return BadRequest("Se ingresaron valores no validos");
            
            string operacion = operacionHeader.ToString().ToLower();

            if(!EsOperacionBasica(operacion))
                return BadRequest("La operacion no es una operacion basica(Suma, Resta, Multiplicacion, Division)");

            decimal resultado = Operaciones(operacion, num1, num2);

            return Ok(resultado);
        }

        private decimal Operaciones(string operacion, decimal num1, decimal num2) 
        {
            decimal resultado = 0;

            switch (operacion)
            {
                case "suma":
                    resultado = num1 + num2;
                    break;
                case "multiplicacion":
                    resultado = num1 * num2;
                    break;
                case "division":
                    resultado = num1 / num2;
                    break;
                case "resta":
                    resultado = num1 - num2;
                    break;
            }

            return resultado;
        }

        private bool EsOperacionBasica(string operacion)
        {
            bool resultado = false;

            if(operacion.Equals("suma") ||
                operacion.Equals("multiplicacion") ||
                operacion.Equals("division") ||
                operacion.Equals("resta"))
                resultado = true;

            return resultado;
        }
    }
}
