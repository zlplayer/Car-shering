using AutoMapper;
using Car_shering.Dtos;
using Car_shering.Entity;
using Car_shering.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Car_shering.Service
{
    public class CarService: ICarService
    {
        private readonly CarSheringDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CarService(CarSheringDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CarDto>> GetAllCars()
        {
            var cars =await _dbcontext.Cars.Where(x=>x.IsAvailable==false).ToListAsync();

            var carDtos = _mapper.Map<IEnumerable<CarDto>>(cars);

            return carDtos;
        }
        public async Task<CarDto>GetById(int id)
        {
            var car = await _dbcontext.Cars.Include(x=>x.CarLocalization).FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return null;
            }

            var carDto = _mapper.Map<CarDto>(car);

            return carDto;
        }
        public async Task Create(CreateCarDto dto)
        {
            var car= _mapper.Map<Car>(dto);
            _dbcontext.Cars.Add(car);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task Update(int id, UpdateCarDto dto)
        {
            var car = await _dbcontext.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return;
            }

            car.Brand = dto.Brand;
            car.Model = dto.Model;
            car.RegistrationNumber = dto.RegistrationNumber;
            car.Color = dto.Color;
            car.Year = dto.Year;
            car.Price = dto.Price;
           

            await _dbcontext.SaveChangesAsync();
        }
       
        public async Task Delete(int id)
        {
            var car = await _dbcontext.Cars.Include(x=>x.CarLocalization).FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return;
            }

            _dbcontext.Cars.Remove(car);

            await _dbcontext.SaveChangesAsync();
        }
        public async Task Rent(int carId, string userId)
        {
            var car = await _dbcontext.Cars.FirstOrDefaultAsync(x => x.Id == carId);

            if (car == null)
            {
                return;
            }
            car.IsAvailable = true;
            var rentalDate = DateTime.Now;
            var returnDate = rentalDate.AddDays(7);
            var rentalPeriod = (returnDate - rentalDate).Days;
            var rent = new Rental
            {
                CarId = carId,
                AppUserId = userId,
                RentalDate = rentalDate,
                ReturnDate = returnDate,
                RentalPrice = car.Price * rentalPeriod
            };

            _dbcontext.Rentals.Add(rent);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<IEnumerable<CarDto>> GetRentedCarsByUserId(string userId)
        {
            var cars = await _dbcontext.Rentals
            .Where(r => r.AppUserId == userId)
            .Include(r => r.Car)  // Włączenie powiązanych obiektów Car
            .Select(r => r.Car)   // Wybranie obiektów Car z wyników
            .ToListAsync();

            // Mapowanie wyników na CarDto
            var carDtos = _mapper.Map<IEnumerable<CarDto>>(cars);

            return carDtos;
        }
       
        public async Task UpdateCarAddress(int id, CarLocalizationDto dto)
        {
            var car = await _dbcontext.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                // Logowanie braku samochodu
                Console.WriteLine($"Samochód z Id {id} nie znaleziono.");
                return;
            }

            var localization = await _dbcontext.CarLocalizations
                .FirstOrDefaultAsync(x => x.Id == car.CarLocalizationId);

            if (localization == null)
            {
                Console.WriteLine($"CarLocalization z CarId {id} nie znaleziono.");
                return ;
            }

            localization.City = dto.City;
            localization.PostalCode = dto.PostalCode;
            localization.NameStread = dto.NameStread;
            localization.NumberStreet = dto.NumberStreet;

            await _dbcontext.SaveChangesAsync();
        }
        public async Task ReturnCar(int carid)
        {
            var rents = await _dbcontext.Rentals
                 .FirstOrDefaultAsync(r => r.CarId == carid);
            var car = await _dbcontext.Cars
                .FirstOrDefaultAsync(x => x.Id == carid);
            car.IsAvailable = false;
                
            if(rents == null)
            {
                Console.WriteLine($"Brak wypożyczenia dla samochodu o Id {carid}.");
                return;
            }
            
           _dbcontext.Rentals.Remove(rents);

            await _dbcontext.SaveChangesAsync();
        }
    }
}
