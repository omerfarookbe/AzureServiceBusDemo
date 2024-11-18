using System.ComponentModel.DataAnnotations;

namespace SBShared.Models
{
    public class Person
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
