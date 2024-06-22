using System.ComponentModel.DataAnnotations.Schema;

namespace Car_shering.Entity
{
    public class Rental
    {
        public int Id { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentalPrice { get; set; }
    }
}
