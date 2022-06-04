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
                ViewBag.Robo = JsonConvert.DeserializeObject<Robo>(resposta);
                return View(ViewBag.Robo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, ActionName("AumentarInclinacao")]
        public IActionResult AumentarInclinacao(Robo robo)
        {
            var teste = JsonConvert.SerializeObject(robo.Cabeca.InclinacaoCabeca.StatusInclinacao++);
            var httpContent = new StringContent(teste, Encoding.UTF8, "application/json");

            var response = client.PostAsync(urlLocalRoboInclCabeca, httpContent).Result; 
            return View(ViewBag.Robo);
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