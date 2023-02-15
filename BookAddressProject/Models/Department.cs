using System.ComponentModel.DataAnnotations;

namespace AddressBookProject.Models
{
    public class Department : BaseClass
    {
        public Department()
        {
            AddressBook = new HashSet<AddresBook>();
        }
        [Required]
        [MaxLength(25)]
        public string? DepName { get; set; }
        public virtual ICollection<AddresBook>? AddressBook { get; set; }
    }
}
