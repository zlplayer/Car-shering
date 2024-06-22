using System.ComponentModel.DataAnnotations;

namespace Car_shering.Entity
{
    public class CarLocalization
    {
        public int Id { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string NameStread { get; set; }
        [Required]
        public int NumberStreet { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

    }
}
