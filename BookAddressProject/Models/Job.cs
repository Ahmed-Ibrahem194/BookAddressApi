
using System.ComponentModel.DataAnnotations;

namespace AddressBookProject.Models
{
    public partial class Job : BaseClass
    {
        public Job()
        {
            AddressBook = new HashSet<AddresBook>();
        }

        [Required]
        [MaxLength(25)]
        public string? JobTitle { get; set; }
        public virtual ICollection<AddresBook>? AddressBook { get; set; }

    }
}
