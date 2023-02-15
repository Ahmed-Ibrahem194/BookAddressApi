using System.ComponentModel.DataAnnotations;

namespace AddressBookProject.Models
{
    public class BaseClass
    {
        [Key]
        public int? Id { get; set; }
    }
}
