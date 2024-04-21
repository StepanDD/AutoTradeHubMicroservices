using AutoTradeHubDataWebApi.Data;
using AutoTradeHubDataWebApi.Interfaces;
using AutoTradeHubDataWebApi.Models;
using AutoTradeHubDataWebApi.RabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AutoTradeHubDataWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarController : ControllerBase
	{
		private readonly ICarRepository _carRepository;
		private readonly IRabbitMqService _rabbitMqService;

		public CarController(ICarRepository carRepository, IRabbitMqService rabbitMqService)
		{
			_carRepository = carRepository;
			_rabbitMqService = rabbitMqService;
		}

		[HttpGet]
		public async Task<ActionResult> GetCars()
		{
			var cars = await _carRepository.GetAll();
			_rabbitMqService.SendMessage(cars, "GetCars");
			Debug.WriteLine("Отправлен список авто");
			return Ok();
		}

		[HttpGet("{marka}")]
		public async Task<IEnumerable<Car>> GetCarsByMarka(string marka)
		{
			return await _carRepository.GetCarByMarka(marka);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetCarById(int id)
		{
			Car car = await _carRepository.GetByIdAsync(id);
			string queueName = "Car" + Convert.ToString(id);
			_rabbitMqService.SendMessage(car, queueName);
			Debug.WriteLine($"Отправлено авто с id: {id}");
			return Ok();
		}

		[HttpPost]
		public ActionResult AddCar(string carJson)
		{
			Debug.WriteLine("Зашли в пост запрос, поздравляю!");

			Car car = JsonSerializer.Deserialize<Car>(carJson);

			if (!ModelState.IsValid) return BadRequest();
			_carRepository.Add(car);
			Debug.WriteLine("Авто добавлено");
			return Ok();
		}

		[HttpPut]
		public ActionResult UpdateCar(Car car)
		{
			if (!ModelState.IsValid) return BadRequest();
			_carRepository.Update(car);
			return Ok();
		}

		[HttpDelete]
		public ActionResult DeleteCar(Car car)
		{
			if (!ModelState.IsValid) return BadRequest();
			_carRepository.Delete(car);
			return Ok();
		}

	}
}
