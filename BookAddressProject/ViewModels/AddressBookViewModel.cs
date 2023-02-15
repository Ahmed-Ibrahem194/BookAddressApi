

namespace BookAddressProject.ViewModels
{
    public class AddressBookViewModel 
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Job { get; set; }
        public string? Department { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public int? Age { get; set; }
        public DateTime? BirthOfDate { get; set; }
    }
}
