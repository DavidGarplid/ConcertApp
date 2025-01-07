using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ConcertApp.Data.Entity
{
    public class User
    {
        [Key]
        [StringLength(36), MinLength(36)]
        public int ID { get; set; }

        [Required]
        [StringLength(25)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string password { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
