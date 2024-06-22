using AutoMapper;
using Car_shering.Dtos;
using Car_shering.Entity;
using Car_shering.Interfaces;
using Car_shering.Models;
using Car_shering.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Car_shering.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarService _carservice;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly CarSheringDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ICarService carService, IMapper mapper, UserManager<AppUser> userManager, CarSheringDbContext dbContext)
        {
            _logger = logger;
            _carservice = carService;
            _mapper = mapper;
            _userManager = userManager;
            _dbContext= dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await _carservice.GetAllCars();
            return View(cars);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("Home/Details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var car = await _carservice.GetById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("/Home/Create")]
        public async Task<IActionResult> Create(CreateCarDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var car = _carservice.Create(carDto);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var car = await _carservice.GetById(id);
            if (car == null)
            {
                return NotFound();
            }
            await _carservice.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet("Home/Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var car = await _carservice.GetById(id);
            if (car == null)
            {
                return NotFound();
            }
            UpdateCarDto model= _mapper.Map<UpdateCarDto>(car);
            return View(model);
        }
        [HttpPost]
        [Route("Home/Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id, UpdateCarDto carDto)
        {
            var car = await _carservice.GetById(id);
            if (car is null)
            {
                return NotFound();
            }
            await _carservice.Update(id, carDto);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("Home/Rent")]
        public async Task<IActionResult> Rent(int carid)
        {
            var userId = _userManager.GetUserId(User);

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _carservice.Rent(carid, userId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Home/UserRentedCars")]
        public async Task<IActionResult> UserRentedCars()
        {
            var userId = _userManager.GetUserId(User);

            var rentedCars = await _carservice.GetRentedCarsByUserId(userId);
            return View(rentedCars);
        }

        [HttpGet]
        [Route("Home/DetailsRentedCar/{id}")]
        public async Task<IActionResult> DetailsRentedCar([FromRoute] int id)
        {
            var car = await _carservice.GetById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpGet("Home/UpdateCarAddress/{id}")]
        public async Task<IActionResult> UpdateCarAddress([FromRoute] int id)
        {
            var car = await _dbContext.Cars
            .Include(x => x.CarLocalization)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var carLocalizationDto = new CarLocalizationDto
            {
                City = car.CarLocalization.City,
                PostalCode = car.CarLocalization.PostalCode,
                NameStread = car.CarLocalization.NameStread,
                NumberStreet = car.CarLocalization.NumberStreet
            };

            ViewBag.CarId = car.Id; 

            return View(carLocalizationDto);
        }

        [HttpPost]
        [Route("Home/UpdateCarAddress/{id}")]
        public async Task<IActionResult> UpdateCarAddress(int id, CarLocalizationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var car = await _carservice.GetById(id);
            if (car == null)
            {
                return NotFound();
            }

            await _carservice.UpdateCarAddress(id, dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ReturnCar()
        {
            return View();
        }
        [HttpPost]
        [Route("Home/ReturnCar/{carid}")]
        public async Task<IActionResult> ReturnCar(int carid)
        {

            await _carservice.ReturnCar(carid);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
