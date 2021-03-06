using BecomexRoboInterfaceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using ApiBecomexRobo.Model;
using System.Text;

namespace BecomexRoboInterfaceWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string urlLocalRobo = "https://localhost:7288/api/Robo";
        private const string urlLocalRoboMovCotovelo = "https://localhost:7288/api/Robo/Cotovelo/Movimentar";
        private const string urlLocalRoboMovPulso = "https://localhost:7288/api/Robo/Pulso/Movimentar";
        private const string urlLocalRoboRotCabeca = "https://localhost:7288/api/Robo/Cabeca/Rotacionar";
        private const string urlLocalRoboInclCabeca = "https://localhost:7288/api/Robo/Cabeca/Inclinar";

        private HttpClient client = new HttpClient();
        private static bool roboCriado = false;
        private static Robo robo;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var response = client.GetAsync(urlLocalRobo).Result;
                var resposta = response.Content.ReadAsStringAsync().Result;
                roboCriado = true;
                var novoRobo = JsonConvert.DeserializeObject<Robo>(resposta);
                robo = novoRobo;
                ViewBag.Robo = robo;
                return View(ViewBag.Robo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, ActionName("AjustarInclinacao")]
        public IActionResult AjustarInclinacao(string button)
        {
            switch (button)
            {
                case "Cima":
                    //quanto menor o status, maior a inclinacao
                    robo.Cabeca.InclinacaoCabeca.StatusInclinacao--;
                    break;
                case "Baixo":
                    //quanto maior o status, menor a inclinação
                    robo.Cabeca.InclinacaoCabeca.StatusInclinacao++;
                    break;
                default:
                    return BadRequest("Status de botão inválido");
            }
            var json = JsonConvert.SerializeObject(robo.Cabeca.InclinacaoCabeca);
            EnviarAlteracoesParaApi(json, urlLocalRoboInclCabeca);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("AjustarRotacao")]
        public IActionResult AjustarRotacao(string button)
        {
            switch (button)
            {
                case "Esquerda":
                    //quanto menor o status, maior a inclinacao
                    robo.Cabeca.RotacaoCabeca.StatusRotacao--;
                    break;
                case "Direita":
                    //quanto maior o status, menor a inclinação
                    robo.Cabeca.RotacaoCabeca.StatusRotacao++;
                    break;
                default:
                    return BadRequest("Status de botão inválido");
            }
            var json = JsonConvert.SerializeObject(robo.Cabeca.RotacaoCabeca);
            EnviarAlteracoesParaApi(json, urlLocalRoboRotCabeca);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("AjustarCotovelo")]
        public IActionResult AjustarCotovelo(string button)
        {
            string? json = null;
            switch (button)
            {
                case "DescontrairD":
                    //quanto menor o status, menor a contração
                    robo.BracoDireito.CotoveloBraco.StatusCotovelo--;
                    robo.BracoDireito.CotoveloBraco.LadoCotovelo = "D";
                    json = JsonConvert.SerializeObject(robo.BracoDireito.CotoveloBraco);
                    EnviarAlteracoesParaApi(json, urlLocalRoboMovCotovelo);
                    break;
                case "ContrairD":
                    //quanto maior o status, maior a contração
                    robo.BracoDireito.CotoveloBraco.StatusCotovelo++;
                    robo.BracoDireito.CotoveloBraco.LadoCotovelo = "D";
                    json = JsonConvert.SerializeObject(robo.BracoDireito.CotoveloBraco);
                    EnviarAlteracoesParaApi(json, urlLocalRoboMovCotovelo);
                    break;
                case "DescontrairE":
                    //quanto maior o status, maior a contração
                    robo.BracoEsquerdo.CotoveloBraco.StatusCotovelo--;
                    robo.BracoEsquerdo.CotoveloBraco.LadoCotovelo = "E";
                    json = JsonConvert.SerializeObject(robo.BracoEsquerdo.CotoveloBraco);
                    EnviarAlteracoesParaApi(json, urlLocalRoboMovCotovelo);
                    break;
                case "ContrairE":
                    //quanto maior o status, maior a contração
                    robo.BracoEsquerdo.CotoveloBraco.StatusCotovelo++;
                    robo.BracoEsquerdo.CotoveloBraco.LadoCotovelo = "E";
                    json = JsonConvert.SerializeObject(robo.BracoEsquerdo.CotoveloBraco);
                    EnviarAlteracoesParaApi(json, urlLocalRoboMovCotovelo);
                    break;
                default:
                    return BadRequest("Status de botão inválido");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("AjustarPulso")]
        public IActionResult AjustarPulso(string button)
        {
            string? json = null;
            //o valor do botão é a junção da função + lado do pulso
            //Ex: Esquerda ou Direita = Lado do braço
            // D ou E = Função para esquerda ou direita
            switch (button)
            {
                case "DireitaD":
                    //braço direito virar para direita
                    robo.BracoDireito.PulsoBraco.StatusPulso++;
                    robo.BracoDireito.PulsoBraco.LadoPulso = "D";
                    json = JsonConvert.SerializeObject(robo.BracoDireito.PulsoBraco);
                    EnviarAlteracoesParaApi(json, urlLocalRoboMovPulso);
                    break;
                case "DireitaE":
                    //braço direito virar para esquerda
                    robo.BracoDireito.PulsoBraco.StatusPulso--;
                    robo.BracoDireito.PulsoBraco.LadoPulso = "D";
                    json = JsonConvert.SerializeObject(robo.BracoDireito.PulsoBraco);
                    EnviarAlteracoesParaApi(json, urlLocalRoboMovPulso);
                    break;
                case "EsquerdaD":
                    //braço esquerdo virar para direita
                    robo.BracoEsquerdo.PulsoBraco.StatusPulso++;
                    robo.BracoEsquerdo.PulsoBraco.LadoPulso = "E";
                    json = JsonConvert.SerializeObject(robo.BracoEsquerdo.PulsoBraco);
                    EnviarAlteracoesParaApi(json, urlLocalRoboMovPulso);
                    break;
                case "EsquerdaE":
                    //braço esquerdo virar para esquerda
                    robo.BracoEsquerdo.PulsoBraco.StatusPulso--;
                    robo.BracoEsquerdo.PulsoBraco.LadoPulso = "E";
                    json = JsonConvert.SerializeObject(robo.BracoEsquerdo.PulsoBraco);
                    EnviarAlteracoesParaApi(json, urlLocalRoboMovPulso);
                    break;
                default:
                    return BadRequest("Status de botão inválido");
            }
            return RedirectToAction(nameof(Index));
        }

        private void EnviarAlteracoesParaApi(string json, string url)
        {
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = client.PostAsync(url, httpContent).Result;
            var response = result.Content.ReadAsStringAsync().Result;
            var RoboResposta = JsonConvert.DeserializeObject<Robo>(response);
            robo = RoboResposta;
            ViewBag.Robo = robo;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}