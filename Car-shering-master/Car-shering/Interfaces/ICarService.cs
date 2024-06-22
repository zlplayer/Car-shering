using Car_shering.Dtos;
using Car_shering.Entity;

namespace Car_shering.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAllCars();
        Task<CarDto> GetById(int id);
        Task Create(CreateCarDto dto);
        Task Update(int id, UpdateCarDto dto);
        Task Delete(int id);
        Task Rent(int carId, string userId);
        Task<IEnumerable<CarDto>> GetRentedCarsByUserId(string userId);
        Task UpdateCarAddress(int id, CarLocalizationDto dto);
        Task ReturnCar(int carid);
    }
}