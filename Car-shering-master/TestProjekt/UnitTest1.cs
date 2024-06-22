using AutoMapper;
using Car_shering;
using Car_shering.Dtos;
using Car_shering.Entity;
using Car_shering.Service;
using Microsoft.EntityFrameworkCore;

namespace TestProjekt
{
    public class UnitTest1:IDisposable
    {
        private readonly CarSheringDbContext _dbContext;
        private readonly IMapper _mapper;
        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<CarSheringDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCarShering")
                .Options;
            _dbContext = new CarSheringDbContext(options);
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(CarSheringMappingProfile));
            }).CreateMapper();
            SeedDatabase();

        }
        private void SeedDatabase()
        {
            _dbContext.Cars.RemoveRange(_dbContext.Cars);
            _dbContext.SaveChanges();

            // Dodanie przyk³adowych danych
            var car1 = new Car
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Corolla",
                Color = "Black",
                RegistrationNumber = "RKL654334",
                CarLocalization = new CarLocalization
                {
                    City = "City1",
                    PostalCode = "00001",
                    NameStread = "Street1",
                    NumberStreet = 1
                }
            };
            var car2 = new Car
            {
                Id = 2,
                Brand = "Honda",
                Model = "Civic",
                Color = "Red",
                RegistrationNumber = "RZ45K32",
                CarLocalization = new CarLocalization
                {
                    City = "City2",
                    PostalCode = "00002",
                    NameStread = "Street2",
                    NumberStreet = 2
                }
            };

            _dbContext.Cars.AddRange(car1, car2);
            _dbContext.SaveChanges();
        }
        //[Fact]
        //public void Test1()
        //{ 
        //    List<Car> cars = new List<Car>() {
        //    new Car
        //    {
        //        Brand = "Audi",
        //        Model = "A4",
        //        RegistrationNumber = "KR12345",
        //        Color = "Black",
        //        Year = 2018,
        //        Price = 100,
        //        IsAvailable = true,
        //        CarLocalization = new CarLocalization
        //        {
        //            City = "Kraków",
        //            PostalCode = "30-001",
        //            NameStread = "Mickiewicza",
        //            NumberStreet = 1s
        //        }
        //    },
        //    };
        //    Assert.Equal(1, cars.Count);
        //}
        [Fact]
        public async Task Test2()
        {
            var cars = await _dbContext.Cars.ToListAsync();
            Assert.NotNull(cars);
            Assert.Equal(2, cars.Count);
        }
        [Fact]
        public async Task GetAllCars_NotNull()
        {
            var carService = new CarService(_dbContext, _mapper);
            var cars = await carService.GetAllCars();
            Assert.NotNull(cars);
        }

        [Fact]
        public async Task GetAllCars_ReturnsCorrectNumberOfCars()
        {
            var carService = new CarService(_dbContext, _mapper);
            var cars = await carService.GetAllCars();
            Assert.Equal(2, cars.Count());
        }

        [Fact]
        public async Task GetById_Null()
        {
            var carService = new CarService(_dbContext, _mapper);
            var car = await carService.GetById(3);
            Assert.Null(car);
        }

        [Fact]
        public async Task GetById_NotNull()
        {
            var carService = new CarService(_dbContext, _mapper);
            var car = await carService.GetById(1);
            Assert.NotNull(car);
        }

        [Fact]
        public async Task GetById_ReturnsCorrectCar()
        {
            var carService = new CarService(_dbContext, _mapper);
            var car = await carService.GetById(1);
            Assert.Equal("Toyota",car.Brand);
        }
        [Fact]
        public async Task Create_NotNull() {             
            var carService = new CarService(_dbContext, _mapper);
            var createCarDto = new CreateCarDto
                   {
                Brand = "Opel",
                Model = "Astra",
                Color = "Blue",
                RegistrationNumber = "KR12345",
                Year = 2018,
                Price = 100,
                City = "Kraków",
                PostalCode = "30-001",
                NameStread = "Mickiewicza",
                NumberStreet = 1
            };
            await carService.Create(createCarDto);
            var car = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Brand == "Opel");
            Assert.NotNull(car);
        }
        [Fact]
        public async Task Create_Equals()
        {
            var carService = new CarService(_dbContext, _mapper);
            var createCarDto = new CreateCarDto
            {
                Brand = "Ford",
                Model = "Mustang",
                Color = "Blue",
                RegistrationNumber = "KR12345",
                Year = 2018,
                Price = 100,
                City = "Kraków",
                PostalCode = "30-001",
                NameStread = "Mickiewicza",
                NumberStreet = 1
            };
            await carService.Create(createCarDto);
            var car = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Brand == "Ford");
            Assert.Equal("Ford", car.Brand);
        }
        [Fact]
        public async Task Update_NotNull()
        {
            var carService = new CarService(_dbContext, _mapper);
            var updateCarDto = new UpdateCarDto
            {
                Brand = "Fiat",
                Model = "150",
                Color = "Blue",
                RegistrationNumber = "KR12345",
                Year = 2018,
                Price = 100
            };
            await carService.Update(1, updateCarDto);
            var car = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Brand == "Fiat");
            Assert.NotNull(car);
        }
        [Fact]
        public async Task Update_Equals()
        {
            var carService = new CarService(_dbContext, _mapper);
            var updateCarDto = new UpdateCarDto
            {
                Brand = "Honda",
                Model = "Accord",
                Color = "Red",
                RegistrationNumber = "RZ45K32",
                Year = 2018,
                Price = 100
            };
            await carService.Update(2, updateCarDto);
            var car = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Model == "Accord");
            Assert.Equal("Accord", car.Model);
        }
        [Fact]
        public async Task Delete_Null()
        {
            var carService = new CarService(_dbContext, _mapper);
            await carService.Delete(1);
            var car = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Id == 1);
            Assert.Null(car);
        }
        [Fact]
        public async Task Rent_NotNull()
        {
            var carService = new CarService(_dbContext, _mapper);
            
            var UserId = "1";
            await carService.Rent(1,UserId);
            var rental = await _dbContext.Rentals.FirstOrDefaultAsync(r => r.CarId == 1);
            Assert.NotNull(rental);
        }
        [Fact]
        public async Task Rent_Equal()
        {
            var carService = new CarService(_dbContext, _mapper);
            var UserId = "1";
            await carService.Rent(1, UserId);
            var rental = await _dbContext.Rentals.FirstOrDefaultAsync(r => r.CarId == 1);
            Assert.Equal("1", rental.AppUserId);
        }

        [Fact]
        public async Task ReturnCar_Null()
        {
            var carService = new CarService(_dbContext, _mapper);
            var UserId = "1";
            await carService.Rent(2, UserId);
            await carService.ReturnCar(2);
            var rental = await _dbContext.Rentals.FirstOrDefaultAsync(r => r.CarId == 2);
            Assert.Null(rental);
        }

        [Fact]
        public async Task UpdateCarAddress_NotNull()
        {
            var carService = new CarService(_dbContext, _mapper);
            var updateCarAddressDto = new CarLocalizationDto
            {
                City = "Kraków",
                PostalCode = "30-001",
                NameStread = "Mickiewicza",
                NumberStreet = 1
            };
            await carService.UpdateCarAddress(1, updateCarAddressDto);
            var car = await _dbContext.Cars.Include(x => x.CarLocalization).FirstOrDefaultAsync(x => x.Id == 1);
            Assert.NotNull(car);
        }
        [Fact]
        public async Task UpdateCarAddress_Equals()
        {
            var carService = new CarService(_dbContext, _mapper);
            var updateCarAddressDto = new CarLocalizationDto
            {
                City = "Warszawa",
                PostalCode = "00-001",
                NameStread = "Mickiewicza",
                NumberStreet = 1
            };
            await carService.UpdateCarAddress(2, updateCarAddressDto);
            var car = await _dbContext.Cars.Include(x => x.CarLocalization).FirstOrDefaultAsync(x => x.Id == 2);
            Assert.Equal("Warszawa", car.CarLocalization.City);
        }
        [Fact]
        public async Task GetRentedCarsByUserId_NotNull()
        {
            var carService = new CarService(_dbContext, _mapper);
            var UserId = "1";
            var cars = await carService.GetRentedCarsByUserId(UserId);
            Assert.NotNull(cars);
        }
        [Fact]
        public async Task GetRentedCarsByUserId_ReturnsCorrectNumberOfCars()
        {
            var carService = new CarService(_dbContext, _mapper);
            var UserId = "1";
            var cars = await carService.GetRentedCarsByUserId(UserId);
            Assert.Equal(0, cars.Count());
        }
        [Fact]
        public void Dispose()
        {
            ((IDisposable)_dbContext).Dispose();
        }
    }
}