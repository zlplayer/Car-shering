using System.ComponentModel.DataAnnotations;

namespace Car_shering.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string NameStread { get; set; }
        public int NumberStreet { get; set; }
        public int CarId { get; set; }
        public List<RentalDto> Rentals { get; set;}
    }
}
