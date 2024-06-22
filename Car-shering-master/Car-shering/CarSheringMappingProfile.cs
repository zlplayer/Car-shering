using AutoMapper;
using Car_shering.Dtos;
using Car_shering.Entity;

namespace Car_shering
{
    public class CarSheringMappingProfile:Profile
    {
        public CarSheringMappingProfile()
        {
            CreateMap<Car, CarDto>().ForMember(m => m.City, opt => opt.MapFrom(src => src.CarLocalization.City))
                .ForMember(m => m.PostalCode, opt => opt.MapFrom(src => src.CarLocalization.PostalCode))
                .ForMember(m => m.NameStread, opt => opt.MapFrom(src => src.CarLocalization.NameStread))
                .ForMember(m => m.NumberStreet, opt => opt.MapFrom(src => src.CarLocalization.NumberStreet));

            CreateMap<CreateCarDto,Car>().ForMember(Car => Car.CarLocalization, opt => opt.MapFrom(src => new CarLocalization
            {
                City = src.City,
                PostalCode = src.PostalCode,
                NameStread = src.NameStread,
                NumberStreet = src.NumberStreet
            }));
            CreateMap<UpdateCarDto, Car>();
            CreateMap<CarDto, UpdateCarDto>();

            CreateMap<CarLocalizationDto, CarLocalization>();
        }
           
    }
}
