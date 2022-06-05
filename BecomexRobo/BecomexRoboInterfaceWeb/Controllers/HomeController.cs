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
                //caso não tenha instanciado um robô, é criado um
                if (roboCriado == false)
                {
                    var response = client.GetAsync(urlLocalRobo).Result;
                    var resposta = response.Content.ReadAsStringAsync().Result;
                    roboCriado = true;
                    var novoRobo = JsonConvert.DeserializeObject<Robo>(resposta);
                    robo = novoRobo;
                    ViewBag.Robo = robo;
                }
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
            return View("Index");
        }

        [HttpPost, ActionName("AjustarRotacao")]
        public IActionResult AjustarRotacao(string button)
        {
            switch (button)
            {
                case "Cima":
                    //quanto menor o status, maior a inclinacao
                    robo.Cabeca.RotacaoCabeca.StatusRotacao--;
                    break;
                case "Baixo":
                    //quanto maior o status, menor a inclinação
                    robo.Cabeca.RotacaoCabeca.StatusRotacao++;
                    break;
                default:
                    return BadRequest("Status de botão inválido");
            }
            var json = JsonConvert.SerializeObject(robo.Cabeca.InclinacaoCabeca);
            EnviarAlteracoesParaApi(json, urlLocalRoboRotCabeca);
            return View("Index");
        }

        private void EnviarAlteracoesParaApi(string json, string url)
        {
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            ViewBag.Robo = robo;
            client.PostAsync(url, httpContent);
        }






        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}