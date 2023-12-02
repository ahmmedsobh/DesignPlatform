using System.ComponentModel.DataAnnotations;

namespace DesignPlatform.ViewModels.CustomerViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
