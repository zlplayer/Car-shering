using Car_shering.Dtos;
using Car_shering.Entity;
using System.Drawing.Text;

namespace Car_shering
{
    public class Seeder
    {
        private readonly CarSheringDbContext _dbContext;
        public Seeder(CarSheringDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public  void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Cars.Any())
                {
                    var cars = GetCars();
                    _dbContext.Cars.AddRange(cars);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Car> GetCars()
        {
            var cars = new List<Car>
                {
                    new Car
                    {
                        Brand = "Audi",
                        Model = "A4",
                        RegistrationNumber = "KR12345",
                        Color = "Black",
                        Year = 2018,
                        Price = 100,
                        IsAvailable = true,
                        CarLocalization = new CarLocalization
                        {
                            City = "Kraków",
                            PostalCode = "30-001",
                            NameStread = "Mickiewicza",
                            NumberStreet = 1
                        }
                    },
                    new Car
                    {
                        Brand = "BMW",
                        Model = "X5",
                        RegistrationNumber = "KR54321",
                        Color = "White",
                        Year = 2019,
                        Price = 150,
                        IsAvailable = true,
                        CarLocalization = new CarLocalization
                        {
                            City = "Kraków",
                            PostalCode = "30-001",
                            NameStread = "Mickiewicza",
                            NumberStreet = 1
                        }
                    },
                    new Car
                    {
                        Brand = "Mercedes",
                        Model = "E220",
                        RegistrationNumber = "KR67890",
                        Color = "Silver",
                        Year = 2017,
                        Price = 120,
                        IsAvailable = true,
                        CarLocalization = new CarLocalization
                        {
                            City = "Kraków",
                            PostalCode = "30-001",
                            NameStread = "Mickiewicza",
                            NumberStreet = 1
                        }
                    }
                };
            return cars;
        }
    }
}

