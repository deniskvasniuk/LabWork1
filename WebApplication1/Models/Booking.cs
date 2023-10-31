using System.ComponentModel.DataAnnotations.Schema;

namespace LabWork1.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string? Photo { get; set; }

        [NotMapped]
        public IFormFile PhotoFile { get; set; }
        public string PhoneNumber { get; set; }

        public string PickUpLocation { get; set; }
        public string DropLocation { get; set; }
    }
}
