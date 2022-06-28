using System.ComponentModel.DataAnnotations;

namespace OfficeLoginWebApp.Models
{
    public class OfficeLogger
    {
        [Key]
        public int LoggerId { get; set; }

        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Display(Name ="Email")]
        public string LoggerEmail { get; set; }

        public DateTime LoginTime { get; set; } = DateTime.UtcNow;

        public string IPAddress { get; set; }

        public string IPAddressLocal { get; set; }

        public DateTime? LogoutTime { get; set; }
    }
}
