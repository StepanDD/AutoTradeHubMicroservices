using AutoTradeHubViewService.Models;
using AutoTradeHubViewService.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AutoTradeHubViewService.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IRabbitMqGetMsgService _rabbitMqGetMsgService;

        public HomeController(ILogger<HomeController> logger, IRabbitMqService rabbitMqService,
			IRabbitMqGetMsgService rabbitMqGetMsgService)
		{
			_logger = logger;
            _rabbitMqService = rabbitMqService;
            _rabbitMqGetMsgService = rabbitMqGetMsgService;
        }

		public async Task<IActionResult> Index()
		{
			_rabbitMqService.SendMessage("", "Car", "GetCars");
			List<Car> cars = new List<Car>();
			string msg = _rabbitMqGetMsgService.GetMessage("GetCars");
			if (msg == "")
			{
				await Task.Delay(3000);
				msg = _rabbitMqGetMsgService.GetMessage("GetCars");
			}
			try
			{
				cars = JsonSerializer.Deserialize<List<Car>>(msg);
				return View(cars);
			}
			catch (Exception)
			{
				return BadRequest();
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
