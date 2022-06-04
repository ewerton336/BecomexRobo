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

        private BLL.ValicadaoAcoes validacaoAcoes = new BLL.ValicadaoAcoes();
        ///<summary>
        ///     Obtém o Robo com todos os status dos seus membros
        /// </summary>
        /// <remarks>
        ///     Exemplo requisição:
        /// 
        ///         GET /api/Robo
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
        ///     Movimenta o Cotovelo
        /// </summary>
        /// <remarks>
        ///     Exemplo requisição:
        /// 
        ///         POST /api/Robo/Cotovelo/Movimentar
        /// </remarks>
        /// <param name="TipoCotovelo">D ou E (Direito ou Esquerdo)</param>
        /// <param name="statusCotovelo">Status 1 ao 4</param>
        /// <response code="200">Ação executada</response>
        /// /// <respnse code = "400"> Status ou Braço Inválido</respnse>
        /// <response code="500">Ocorreu algo inesperado. Tente novamente mais tarde.</response>
        // POST : RoboController
        [HttpPost]
        [Route("Cotovelo/Movimentar")]
        public IActionResult MovimentarCotovelo(string ladoCotovelo, int statusNovo)
        {
            //validar status de 1 a 4, que é olimite
            if (statusNovo < 1 || statusNovo > 4)
            {
                return BadRequest("Status do Cotovelo Inválido! O status deve ser de 1 a 4.");
            }
            bool resultadoValidacao;
            //tratamento de acordo com o lado do cotovelo
            switch (ladoCotovelo)
            {
                case "D":
                    //verificar se o status enviado é o mesmo do status atual
                    if (statusNovo == RoboBecomex.BracoDireto.CotoveloBraco.StatusCotovelo)
                        return BadRequest("Você está enviando o mesmo estado de cotovelo que o atual do robô.");
                    //validacao se não está pulando nenhum status de movimentação
                    resultadoValidacao = validacaoAcoes.ValidarMovimentacao(RoboBecomex.BracoDireto.CotoveloBraco.StatusCotovelo, statusNovo);
                    if (resultadoValidacao)
                    {
                        RoboBecomex.BracoDireto.CotoveloBraco.StatusCotovelo = statusNovo;
                        return Ok();
                    }
                    else return BadRequest($"Você está tentando pular um estado! Estado Atual : {RoboBecomex.BracoDireto.CotoveloBraco.StatusCotovelo}. Estado Enviado: {statusNovo}");
                case "E":
                    //verificar se o status enviado é o mesmo do status atual
                    if (statusNovo == RoboBecomex.BracoEsquerdo.CotoveloBraco.StatusCotovelo)
                        return BadRequest("Você está enviando o mesmo estado de cotovelo que o atual do robô.");  
                    //validacao se não está pulando nenhum status de movimentação
                    resultadoValidacao = validacaoAcoes.ValidarMovimentacao(RoboBecomex.BracoEsquerdo.CotoveloBraco.StatusCotovelo, statusNovo);
                    if (resultadoValidacao)
                    {
                        RoboBecomex.BracoEsquerdo.CotoveloBraco.StatusCotovelo = statusNovo;
                        return Ok();
                    }
                    else return BadRequest($"Você está tentando pular um estado! Estado Atual : {RoboBecomex.BracoEsquerdo.CotoveloBraco.StatusCotovelo}. Estado Enviado: {statusNovo}");
                default:
                    return BadRequest("Cotovelo Inválido! Você Deve enviar D ou E (Direito ou Esquerdo).");
            }
        }


        ///<summary>
        ///     Movimenta o Pulso
        /// </summary>
        /// <remarks>
        ///     Exemplo requisição:
        /// 
        ///         POST /api/Robo/Pulso/Movimentar
        /// </remarks>
        /// <param name="ladoPulso">D ou E (Direito ou Esquerdo)</param>
        /// <param name="statusNovo">Status 1 ao 4</param>
        /// <response code="200">Ação executada</response>
        /// /// <respnse code = "400"> Status ou Braço Inválido</respnse>
        /// <response code="500">Ocorreu algo inesperado. Tente novamente mais tarde.</response>
        // POST : RoboController
        [HttpPost]
        [Route("Pulso/Movimentar")]
        public IActionResult MovimentarPulso(string ladoPulso, int statusNovo)
        {
            //validar status de 1 a 7, que é olimite
            if (statusNovo < 1 || statusNovo > 7)
            {
                return BadRequest("Status do Pulso Inválido! O status deve ser de 1 a 7.");
            }
            bool resultadoValidacao;
            bool cotoveloFortementeContraido;
            //tratamento de acordo com o lado do cotovelo
            switch (ladoPulso)
            {
                case "D":
                    //verificar se o status enviado é o mesmo do status atual
                    if (statusNovo == RoboBecomex.BracoDireto.PulsoBraco.StatusPulso)
                        return BadRequest("Você está enviando o mesmo estado de pulso que o atual do robô.");
                    //verificar se o Cotovelo está fortemente contraído
                    cotoveloFortementeContraido = validacaoAcoes.ValidarContracaoCotovelo(RoboBecomex.BracoDireto.CotoveloBraco.StatusCotovelo);
                    if (cotoveloFortementeContraido == false)
                        return BadRequest($"Para movimentar o pulso direito o cotovelo deve estar fortemente contraído. Atualmente está {RoboBecomex.BracoDireto.CotoveloBraco.DescricaoStatusCotovelo}");
                    //validacao se não está pulando nenhum status de movimentação
                    resultadoValidacao = validacaoAcoes.ValidarMovimentacao(RoboBecomex.BracoDireto.PulsoBraco.StatusPulso, statusNovo);
                    if (resultadoValidacao)
                    {
                        RoboBecomex.BracoDireto.PulsoBraco.StatusPulso = statusNovo;
                        return Ok();
                    }
                    else return BadRequest($"Você está tentando pular um estado! Estado Atual : {RoboBecomex.BracoDireto.PulsoBraco.StatusPulso}. Estado Enviado: {statusNovo}");
                case "E":
                    //verificar se o status enviado é o mesmo do status atual
                    if (statusNovo == RoboBecomex.BracoEsquerdo.PulsoBraco.StatusPulso)
                        return BadRequest("Você está enviando o mesmo estado de pulso que o atual do robô.");
                    //verificar se o Cotovelo está fortemente contraído
                    cotoveloFortementeContraido = validacaoAcoes.ValidarContracaoCotovelo(RoboBecomex.BracoEsquerdo.CotoveloBraco.StatusCotovelo);
                   if (cotoveloFortementeContraido == false)
                        return BadRequest($"Para movimentar o pulso esquerdo o cotovelo deve estar fortemente contraído. Atualmente está {RoboBecomex.BracoEsquerdo.CotoveloBraco.DescricaoStatusCotovelo}");
                    //validacao se não está pulando nenhum status de movimentação
                    resultadoValidacao = validacaoAcoes.ValidarMovimentacao(RoboBecomex.BracoEsquerdo.PulsoBraco.StatusPulso, statusNovo);
                    if (resultadoValidacao)
                    {
                        RoboBecomex.BracoEsquerdo.PulsoBraco.StatusPulso = statusNovo;
                        return Ok();
                    }
                    else return BadRequest($"Você está tentando pular um estado! Estado Atual : {RoboBecomex.BracoEsquerdo.PulsoBraco.StatusPulso}. Estado Enviado: {statusNovo}");
                default:
                    return BadRequest("Pulso Inválido! Você Deve enviar D ou E (Direito ou Esquerdo).");
            }
        }



        ///<summary>
        ///     Rotacionar Cabeça
        /// </summary>
        /// <remarks>
        ///     Exemplo requisição:
        /// 
        ///         POST /api/Robo/Cabeca/Rotacionar
        /// </remarks>
        /// <param name="statusNovo">Status 1 ao 5</param>
        /// <response code="200">Ação executada</response>
        /// <respnse code = "400"> Status Inválido</respnse>
        /// <response code="500">Ocorreu algo inesperado. Tente novamente mais tarde.</response>
        // POST : RoboController
        [HttpPost]
        [Route("Cabeca/Rotacionar")]
        public IActionResult RotacionarCabeca(int statusNovo)
        {
            //validar status de 1 a 5, que é olimite
            if (statusNovo < 1 || statusNovo > 5)
            {
                return BadRequest("Status de rotação de Cabeça Inválido! O status deve ser de 1 a 5.");
            }
            //verificar se o status enviado é o mesmo do status atual
            if (statusNovo == RoboBecomex.Cabeca.RotacaoCabeca.StatusRotacao)
                return BadRequest("Você está enviando o mesmo estado de rotação que o atual do robô.");
            bool resultadoValidacao;
            bool inclinacaoParaBaixo;
            //verificar se a inclinação está para baixo
            inclinacaoParaBaixo = validacaoAcoes.ValidarInclinacaoParaBaixo(RoboBecomex.Cabeca.InclinacaoCabeca.StatusInclinacao);
            if (inclinacaoParaBaixo == true)
                return BadRequest($"Para Rotacionar a cabeça do robô a inclinação não deve estar para baixo.");
                    //validacao se não está pulando nenhum status de movimentação
                    resultadoValidacao = validacaoAcoes.ValidarMovimentacao(RoboBecomex.Cabeca.RotacaoCabeca.StatusRotacao, statusNovo);
                    if (resultadoValidacao)
                    {
                        RoboBecomex.Cabeca.RotacaoCabeca.StatusRotacao = statusNovo;
                        return Ok();
                    }
                    else return BadRequest($"Você está tentando pular um estado! Estado Atual : {RoboBecomex.Cabeca.InclinacaoCabeca.StatusInclinacao}. Estado Enviado: {statusNovo}");
        }


        ///<summary>
        ///     Inclinar a Cabeça
        /// </summary>
        /// <remarks>
        ///     Exemplo requisição:
        /// 
        ///         POST /api/Robo/Cabeca/Inclinar
        /// </remarks>
        /// <param name="statusCotovelo">Status 1 ao 5</param>
        /// <response code="200">Ação executada</response>
        /// <respnse code = "400"> Status ou Braço Inválido</respnse>
        /// <response code="500">Ocorreu algo inesperado. Tente novamente mais tarde.</response>
        // POST : RoboController
        [HttpPost]
        [Route("Cabeca/Rotacionar/Inclinar")]
        public IActionResult InclinarCabeca(int statusNovo)
        {
            //validar status de 1 a 3 que é o limite
            if (statusNovo < 1 || statusNovo > 3)
            {
                return BadRequest("Status de rotação de Cabeça Inválido! O status deve ser de 1 a 3.");
            }
            //verificar se o status enviado é o mesmo do status atual
            if (statusNovo == RoboBecomex.Cabeca.InclinacaoCabeca.StatusInclinacao)
                return BadRequest("Você está enviando o mesmo estado de inclinação que o atual do robô.");
            bool resultadoValidacao;
            //validacao se não está pulando nenhum status de movimentação
            resultadoValidacao = validacaoAcoes.ValidarMovimentacao(RoboBecomex.Cabeca.InclinacaoCabeca.StatusInclinacao, statusNovo);
            if (resultadoValidacao)
            {
                RoboBecomex.Cabeca.InclinacaoCabeca.StatusInclinacao = statusNovo;
                return Ok();
            }
            else return BadRequest($"Você está tentando pular um estado! Estado Atual : {RoboBecomex.Cabeca.InclinacaoCabeca.StatusInclinacao}. Estado Enviado: {statusNovo}");
        }
    }
}

