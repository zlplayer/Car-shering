namespace Car_shering.Dtos
{
    public class RentalDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
    }
}
