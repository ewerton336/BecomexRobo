using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiBecomexRobo.Model;
using Newtonsoft.Json;

namespace ApiBecomexRobo.Controllers
{
    [ApiController]
    [Route("api/Robo")]
    [Produces("application/json")]
    public class RoboController : Controller
    {
      
        ///<summary>
        ///     Obtém o Robo com todos os status dos seus membros
        /// </summary>
        /// <remarks>
        ///     Exemplo requisição:
        /// 
        ///         GET /api/robo
        /// </remarks>
        /// <response code="200">Robô Encontrado.</response>
        /// <response code="500">Ocorreu algo inesperado. Tente novamente mais tarde.</response>
        [HttpGet]
        // GET: RoboController
        public ActionResult GetRobo()
        {
            try
            {
                var robo = new Robo();
               
                return Ok(roboJson);
            }
            catch (Exception)
            {

                throw;
            }
        }

       

       

      
    }
}
