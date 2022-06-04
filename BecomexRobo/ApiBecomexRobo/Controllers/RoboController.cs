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
        private static Robo? robo;

        Robo RoboBecomex

        {
            get
            {
                if (robo == null)
                {
                    robo = new Robo();
                }
                return robo;
            }
            set
            {
                robo = value;
            }
        }

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
        public IActionResult GetRobo()
        {
            try
            {
                var RoboJson = Newtonsoft.Json.JsonConvert.SerializeObject(RoboBecomex);
                return Ok(RoboJson);
            }
            catch (Exception)
            {

                throw;
            }
        }

        ///<summary>
        ///     Modifica o status do cotovelo
        /// </summary>
        /// <remarks>
        ///     Exemplo requisição:
        /// 
        ///         POST /api/robo
        /// </remarks>
        /// <param name="TipoCotovelo">D ou E (Direito ou Esquerdo)</param>
        /// <param name="statusCotovelo">Status 1 ao 4</param>
        /// <response code="200">Robô Encontrado.</response>
        /// <respnse code = "400"> Status ou Braço Inválido</respnse>
        /// <response code="500">Ocorreu algo inesperado. Tente novamente mais tarde.</response>
        [HttpPost]
        // POST : RoboController
        public IActionResult DefinirCotovelo(string ladoCotovelo, int statusNovo)
        {
            //validar status de 1 a 4, que é olimite
            if (statusNovo < 1 || statusNovo > 4)
            {
                return BadRequest("Status do Cotovelo Inválido! O status deve ser de 1 a 4.");
            }
            var validacaoRotacao = new BLL.ValicadaoAcoes();
            bool resultadoValidacao;
            //tratamento de acordo com o lado do cotovelo
            switch (ladoCotovelo)
            {
                case "D":
                    //validacao se não está pulando nenhum status de movimentação
                    resultadoValidacao = validacaoRotacao.ValidarMovimentacao(RoboBecomex.BracoDireto.CotoveloBraco.StatusCotovelo, statusNovo);
                    if (resultadoValidacao)
                    {
                        RoboBecomex.BracoDireto.CotoveloBraco.StatusCotovelo = statusNovo;
                        return Ok();
                    }
                    else return BadRequest("Você está tentando pular um estado!");
                case "E":
                    //validacao se não está pulando nenhum status de movimentação
                    resultadoValidacao = validacaoRotacao.ValidarMovimentacao(RoboBecomex.BracoEsquerdo.CotoveloBraco.StatusCotovelo, statusNovo);
                    if (resultadoValidacao)
                    {
                        RoboBecomex.BracoDireto.CotoveloBraco.StatusCotovelo = statusNovo;
                        return Ok();
                    }
                    else return BadRequest("Você está tentando pular um estado!");
                default:
                    return BadRequest("Cotovelo Inválido! Você Deve enviar D ou E (Direito ou Esquerdo).");
            }
        }
    }
}
