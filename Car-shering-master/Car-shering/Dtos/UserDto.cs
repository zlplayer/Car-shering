using System.ComponentModel.DataAnnotations;

namespace Car_shering.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public bool License { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string NameStread { get; set; }
        public int NumberStreet { get; set; }
    }
}
