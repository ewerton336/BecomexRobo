using BecomexRoboInterfaceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using ApiBecomexRobo.Model;

namespace BecomexRoboInterfaceWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string urlLocal = "https://localhost:7288/api/Robo";

        private HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var response = client.GetAsync(urlLocal).Result;
                var resposta = response.Content.ReadAsStringAsync().Result;
                var teste = JsonConvert.DeserializeObject<List<Robo>>(resposta, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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