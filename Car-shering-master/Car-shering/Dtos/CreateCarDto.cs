namespace Car_shering.Dtos
{
    public class CreateCarDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string RegistrationNumber { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public int Price { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string NameStread { get; set; }
        public int NumberStreet { get; set; }
    }
}
