using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_shering.Entity
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        [Required]
        public int Price { get; set; }
        public bool IsAvailable { get; set; }

        public IEnumerable<Rental> Rentals { get; set; }

        [ForeignKey("CarLocalization")]
        public int CarLocalizationId { get; set; }
        public CarLocalization CarLocalization { get; set; }
    }
}
