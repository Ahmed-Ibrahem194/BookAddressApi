using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBookProject.Models
{
    public class AddresBook : BaseClass
    {
        [Required(ErrorMessage ="The name is Required")]
        public string? FullName { get; set; }
        [MaxLength(11)]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        [MaxLength(2)]
        public int? Age { get; set; }
        public DateTime? BirthOfDate { get; set; }
        [ForeignKey("Job")]
        public int jobId { get; set; }
        public virtual Job? job { get; set; }
        [ForeignKey("Department")]
        public int departmentId { get; set; }
        public virtual Department? department { get; set; }

    }
}
