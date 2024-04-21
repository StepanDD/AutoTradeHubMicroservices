using AutoTradeHubViewService.Models;
using AutoTradeHubViewService.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AutoTradeHubViewService.Controllers
{
	public class CarController : Controller
	{
		private readonly IRabbitMqService _rabbitMqService;
		private readonly IRabbitMqGetMsgService _rabbitMqGetMsgService;

		public CarController(IRabbitMqService rabbitMqService, IRabbitMqGetMsgService rabbitMqGetMsgService)
        {
			_rabbitMqService = rabbitMqService;
			_rabbitMqGetMsgService = rabbitMqGetMsgService;
		}
        public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Detail(int id)
		{
			_rabbitMqService.SendMessage(Convert.ToString(id), "Car", "GetCarById");
			Car car = new Car();
			string queueName = "Car" + Convert.ToString(id);
			string msg = _rabbitMqGetMsgService.GetMessage(queueName);
			if (msg == "")
			{
				await Task.Delay(3000);
				msg = _rabbitMqGetMsgService.GetMessage(queueName);
			}
			try
			{
				car = JsonSerializer.Deserialize<Car>(msg);
				return View(car);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(Car car)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			_rabbitMqService.SendMessage(car, "Car", "AddCar");
			return RedirectToAction("Index", "Home");
		}
	}
}
